using Application.Interface;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _repositorio;
        private readonly HttpClient _httpClient;

        public LogService(ILogRepository repositorio, HttpClient httpClient)
        {
            _repositorio = repositorio;
            _httpClient = httpClient;
        }

        // Método para converter um log em formato específico
        public string ConverterLog(string logOriginal)
        {
            var linhas = logOriginal.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var resultado = new List<string>
            {
                "#Version: 1.0",
                "#Date: " + DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss"),
                "#Fields: provider http-method status-code uri-path time-taken response-size cache-status"
            };

            foreach (var linha in linhas)
            {
                var partes = linha.Split('|');

                if (partes.Length == 5)
                {
                    var provider = "\"MINHA CDN\"";
                    var metodoHttp = partes[3].Split(' ')[0].Trim('"');
                    var codigoStatus = partes[1];
                    var uriPath = partes[3].Split(' ')[1];
                    var tempo = Math.Round(double.Parse(partes[4]), 0).ToString();
                    var tamanhoResposta = partes[0];
                    var statusCache = partes[2] == "INVALIDATE" ? "REFRESH_HIT" : partes[2];

                    resultado.Add($"{provider} {metodoHttp} {codigoStatus} {uriPath} {tempo} {tamanhoResposta} {statusCache}");
                }
            }

            return string.Join('\n', resultado);
        }

        // Obter log a partir de uma URL
        public async Task<string> ObterLogDeUrlAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // Transformar e exportar log para arquivo
        public async Task<string> TransformarEExportarAsync(string logOriginal, string caminhoArquivo)
        {
            var logTransformado = ConverterLog(logOriginal);
            await File.WriteAllTextAsync(caminhoArquivo, logTransformado);
            return caminhoArquivo;
        }

        // Obter log transformado pelo ID
        public async Task<Log> ObterLogTransformadoPorIdAsync(int id)
        {
            return await _repositorio.ObterPorIdAsync(id);
        }

        // Obter todos os logs transformados
        public async Task<List<Log>> ObterTodosLogsTransformadosAsync()
        {
            return await _repositorio.ObterTodosAsync();
        }

        // Salvar log transformado
        public async Task<string> SalvarLogAsync(string logOriginal)
        {
            var logTransformado = ConverterLog(logOriginal);
            var log = new Log
            {
                FormatoOriginal = logOriginal,
                FormatoTransformado = logTransformado
            };

            return await _repositorio.AdicionarAsync(log);
        }

        // Remover log pelo ID
        public async Task RemoverAsync(int id)
        {
            await _repositorio.RemoverAsync(id);
        }
    }
}

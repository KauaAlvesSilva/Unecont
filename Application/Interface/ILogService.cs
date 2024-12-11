using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ILogService
    {
        string ConverterLog(string logOriginal);
        Task<string> ObterLogDeUrlAsync(string url);
        Task<string> TransformarEExportarAsync(string logOriginal, string caminhoArquivo);
        Task<Log> ObterLogTransformadoPorIdAsync(int id);
        Task<List<Log>> ObterTodosLogsTransformadosAsync();
        Task<string> SalvarLogAsync(string logOriginal);
        Task RemoverAsync(int id);
    }
}

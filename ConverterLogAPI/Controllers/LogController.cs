using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        // 1. Obter log por ID
        [HttpGet("ObterPorId")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var log = await _logService.ObterLogTransformadoPorIdAsync(id);
            if (log == null)
                return NotFound(new { Mensagem = "Log não encontrado." });

            return Ok(new { log.FormatoOriginal, log.FormatoTransformado });
        }

        // 2. Buscar todos os logs salvos
        [HttpGet("BuscarLogsSalvos")]
        public async Task<IActionResult> BuscarLogsSalvos()
        {
            var logs = await _logService.ObterTodosLogsTransformadosAsync();
            if (!logs.Any())
                return NotFound(new { Mensagem = "Nenhum log salvo encontrado." });

            return Ok(logs.Select(l => new
            {
                l.Id,
                l.FormatoOriginal,
                l.FormatoTransformado,
                l.DataCriacao
            }));
        }

        // 3. Transformar log (entrada manual ou ID)
        [HttpPost("TransformarLog")]
        public async Task<IActionResult> TransformarLog(
            [FromQuery] int? id = null,
            [FromBody] string logOriginal = null,
            [FromQuery] string formatoSaida = " resposta",
            [FromQuery] string caminhoArquivo = null)
        {
            try
            {
                string logEntrada;

                if (id.HasValue)
                {
                    var logSalvo = await _logService.ObterLogTransformadoPorIdAsync(id.Value);
                    if (logSalvo == null)
                        return NotFound(new { Mensagem = "Log não encontrado pelo ID fornecido." });
                    logEntrada = logSalvo.FormatoOriginal;
                }
                else if (!string.IsNullOrEmpty(logOriginal))
                {
                    logEntrada = logOriginal;
                }
                else
                {
                    return BadRequest(new { Mensagem = "É necessário fornecer um ID ou o log original." });
                }

                var logTransformado = _logService.ConverterLog(logEntrada);

                if (formatoSaida.Equals("arquivo", StringComparison.OrdinalIgnoreCase))
                {
                    var caminhoFinal = !string.IsNullOrEmpty(caminhoArquivo)
                        ? caminhoArquivo
                        : Path.Combine(
                            Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\")),
                            "Arquivo",
                            $"log-transformado-{DateTime.Now:yyyyMMddHHmmss}.txt");

                    Directory.CreateDirectory(Path.GetDirectoryName(caminhoFinal));

                    var caminhoSalvo = await _logService.TransformarEExportarAsync(logTransformado, caminhoFinal);
                    return Ok(new { CaminhoArquivo = caminhoSalvo });
                }

                return Ok(new { LogTransformado = logTransformado });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao processar a transformação.", Detalhes = ex.Message });
            }
        }

        // 4. Transformar log obtido de URL
        [HttpPost("TransformarDeUrl")]
        public async Task<IActionResult> TransformarDeUrl(
            [FromQuery] string url,
            [FromQuery] string caminhoArquivo = null)
        {
            try
            {
                var logOriginal = await _logService.ObterLogDeUrlAsync(url);
                var logTransformado = _logService.ConverterLog(logOriginal);

                if (!string.IsNullOrEmpty(caminhoArquivo))
                {
                    var caminhoSalvo = await _logService.TransformarEExportarAsync(logTransformado, caminhoArquivo);
                    return Ok(new { CaminhoArquivo = caminhoSalvo });
                }

                return Ok(new { LogTransformado = logTransformado });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao processar a transformação de URL.", Detalhes = ex.Message });
            }
        }

        // 5. Salvar log original
        [HttpPost("SalvarLog")]
        public async Task<IActionResult> SalvarLog([FromBody] string logOriginal)
        {
            await _logService.SalvarLogAsync(logOriginal);
            return CreatedAtAction(nameof(BuscarLogsSalvos), null);
        }

        // 6. Remover log por ID
        [HttpDelete("RemoverLog")]
        public async Task<IActionResult> RemoverLog(int id)
        {
            try
            {
                await _logService.RemoverAsync(id);
                return StatusCode(200, new { Mensagem = "Deletado com sucesso" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro ao deletar.", Detalhes = ex.Message });

            }
        }
    }
}

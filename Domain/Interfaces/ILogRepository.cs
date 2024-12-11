using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ILogRepository
    {
        Task<Log> ObterPorIdAsync(int id);
        Task<List<Log>> ObterTodosAsync();
        Task<string> AdicionarAsync(Log log);
        Task AtualizarAsync(Log log);
        Task RemoverAsync(int id);
    }
}

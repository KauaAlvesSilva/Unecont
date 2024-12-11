using Domain.Entities;
using Domain.Repositories;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class LogRepositorio : ILogRepository
    {
        private readonly ApplicationDbContext _context;

        public LogRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Log> ObterPorIdAsync(int id)
        {
            return await _context.Logs.FindAsync(id);
        }

        public async Task<List<Log>> ObterTodosAsync()
        {
            return await _context.Logs.ToListAsync();
        }

        public async Task<string> AdicionarAsync(Log log)
        {
            log.DataCriacao = DateTime.UtcNow;
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
            return log.FormatoTransformado;
        }

        public async Task AtualizarAsync(Log log)
        {
            _context.Logs.Update(log);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var log = await _context.Logs.FindAsync(id);
            if (log != null)
            {
                _context.Logs.Remove(log);
                await _context.SaveChangesAsync();
            }
        }
    }
}

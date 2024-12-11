using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Log> Logs { get; set; }
    }
}

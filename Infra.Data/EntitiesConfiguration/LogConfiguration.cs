using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Infra.Data.EntitiesConfiguration
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FormatoOriginal).IsRequired();

            builder.Property(u => u.FormatoTransformado);

            builder.Property(u => u.DataCriacao);

        }
    }

}


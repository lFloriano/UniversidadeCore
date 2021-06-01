using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Universidade.Core.Entidades;

namespace Universidade.Infrastructure.Data.Mapping
{
    public class EstudanteMapping : IEntityTypeConfiguration<Estudante>
    {
        public void Configure(EntityTypeBuilder<Estudante> builder)
        {
            builder.ToTable("tbEstudantes");
            builder.HasKey(x => x.EstudanteID);
            builder.Property(x => x.Nome)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.SobreNome)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(x => x.DataCriacao)
                .IsRequired();
            builder.HasMany(x => x.Matriculas)
                .WithOne(y => y.Estudante)
                .HasForeignKey(x => x.EstudanteID);
        }
    }
}

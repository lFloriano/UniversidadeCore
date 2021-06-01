using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universidade.Core.Entidades;

namespace Universidade.Infrastructure.Data.Mapping
{
    public class DepartamentoMapping : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.ToTable("tbDepartamentos");
            builder.HasKey(x => x.DepartamentoID);
            builder.Property(x => x.Nome)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.Supervisor)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}

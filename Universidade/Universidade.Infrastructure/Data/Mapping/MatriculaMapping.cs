using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universidade.Core.Entidades;

namespace Universidade.Infrastructure.Data.Mapping
{
    public class MatriculaMapping : IEntityTypeConfiguration<Matricula>
    {
        public void Configure(EntityTypeBuilder<Matricula> builder)
        {
            builder.ToTable("tbMatriculas");
            builder.Property(x => x.Nota)
                .HasMaxLength(10)
                .IsRequired(false);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universidade.Core.Entidades;

namespace Universidade.Infrastructure.Data.Mapping
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("tbCursos");
            builder.HasKey(x => x.CursoID);
            builder.Property(x => x.Titulo)
                .HasMaxLength(200);
            builder.Property(x => x.Creditos);
            builder.HasMany(x => x.Matriculas)
                .WithOne(y => y.Curso)
                .HasForeignKey(x => x.CursoID)
                .HasConstraintName("fk_cursoMatricula");
            builder.HasOne<Departamento>(x => x.Departamento)
                .WithMany(x => x.Cursos)
                .HasForeignKey(x => x.DepartamentoID);
            builder.Property(x => x.LotacaoAlunos);
        }
    }
}

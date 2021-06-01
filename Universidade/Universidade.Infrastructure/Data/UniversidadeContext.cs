using Microsoft.EntityFrameworkCore;
using Universidade.Core.Entidades;
using Universidade.Infrastructure.Data.Mapping;

namespace Universidade.Infrastructure.Data
{
    public class UniversidadeContext : DbContext
    {
        public UniversidadeContext(DbContextOptions<UniversidadeContext> options) : base(options) { }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Estudante> Estudantes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EstudanteMapping());
            modelBuilder.ApplyConfiguration(new CursoMapping());
            modelBuilder.ApplyConfiguration(new DepartamentoMapping());
            modelBuilder.ApplyConfiguration(new MatriculaMapping());
        }
    }
}

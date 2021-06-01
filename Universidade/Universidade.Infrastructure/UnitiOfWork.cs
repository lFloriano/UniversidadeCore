using System;
using Universidade.Core.Interfaces;
using Universidade.Infrastructure.Data;

namespace Universidade.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniversidadeContext _context;
        public ICursoRepository Cursos { get; }
        public IDepartamentoRepository Departamentos { get; }
        public IEstudanteRepository Estudantes { get; }
        public IMatriculaRepository Matriculas { get; }

        public UnitOfWork(
            UniversidadeContext contexto,
            ICursoRepository cursos,
            IDepartamentoRepository departamentos,
            IEstudanteRepository estudantes,
            IMatriculaRepository matriculas
            )
        {
            this._context = contexto;
            this.Cursos = cursos;
            this.Departamentos = departamentos;
            this.Estudantes = estudantes;
            this.Matriculas = matriculas;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}

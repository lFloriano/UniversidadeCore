using System;
using Universidade.Core.Interfaces;

namespace Universidade.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        ICursoRepository Cursos { get; }
        IDepartamentoRepository Departamentos { get; }
        IEstudanteRepository Estudantes { get; }
        IMatriculaRepository Matriculas { get; }
        int Complete();
    }
}

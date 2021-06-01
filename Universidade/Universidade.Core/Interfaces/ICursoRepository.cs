using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Core.Entidades;

namespace Universidade.Core.Interfaces
{
    public interface ICursoRepository : IRepository<Curso>
    {
        Task<Curso> BuscarCursoDepartamentoEMatriculasPorCursoId(int id);
        Task<List<Curso>> ListarCursosDepartamentoEMatriculas();
    }
}

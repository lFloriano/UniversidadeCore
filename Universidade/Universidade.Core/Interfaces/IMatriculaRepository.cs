using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Core.Entidades;

namespace Universidade.Core.Interfaces
{
    public interface IMatriculaRepository : IRepository<Matricula>
    {
        Task<Matricula> BuscarMatriculaCompletaPorID(int id);
        Task<List<Matricula>> ListarMatriculasCompletas();
    }
}

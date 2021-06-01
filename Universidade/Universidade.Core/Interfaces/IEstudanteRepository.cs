using System.Threading.Tasks;
using Universidade.Core.Entidades;

namespace Universidade.Core.Interfaces
{
    public interface IEstudanteRepository : IRepository<Estudante>
    {
        Task<Estudante> BuscarEstudanteEMatriculasPorEstudanteId(int id);
    }
}

using Universidade.Core.Entidades;
using Universidade.Core.Interfaces;
using Universidade.Infrastructure.Data;

namespace Universidade.Infrastructure.Repositorio
{
    public class DepartamentoRepository : RepositorioGenerico<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(UniversidadeContext context) : base(context)
        {
        }
    }
}

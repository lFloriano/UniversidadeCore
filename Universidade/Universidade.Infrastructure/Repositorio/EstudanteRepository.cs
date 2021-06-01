using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Core.Entidades;
using Universidade.Core.Interfaces;
using Universidade.Infrastructure.Data;

namespace Universidade.Infrastructure.Repositorio
{
    public class EstudanteRepository : RepositorioGenerico<Estudante>, IEstudanteRepository
    {
        public EstudanteRepository(UniversidadeContext context) : base(context)
        {
        }

        public async Task<Estudante> BuscarEstudanteEMatriculasPorEstudanteId(int id)
        {
            return await _context.Set<Estudante>()
                .Include(x => x.Matriculas)
                .ThenInclude(x => x.Curso)
                .SingleOrDefaultAsync(x => x.EstudanteID == id);
        }
    }
}

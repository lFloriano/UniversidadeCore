using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Core.Entidades;
using Universidade.Core.Interfaces;
using Universidade.Infrastructure.Data;

namespace Universidade.Infrastructure.Repositorio
{
    public class MatriculaRepository : RepositorioGenerico<Matricula>, IMatriculaRepository
    {
        public MatriculaRepository(UniversidadeContext context) : base(context)
        {
        }

        public async Task<Matricula> BuscarMatriculaCompletaPorID(int id)
        {
            return await _context.Set<Matricula>()
                .Include(x=> x.Curso)
                .Include(x=> x.Estudante)
                .SingleOrDefaultAsync(x => x.CursoID == id);
        }

        public async Task<List<Matricula>> ListarMatriculasCompletas()
        {
            return await _context.Set<Matricula>()
                .Include(x => x.Curso)
                .Include(x => x.Estudante)
                .ToListAsync();
        }
    }
}

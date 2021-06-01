using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Universidade.Core.Entidades;
using Universidade.Core.Interfaces;
using Universidade.Infrastructure.Data;

namespace Universidade.Infrastructure.Repositorio
{
    public class CursoRepository : RepositorioGenerico<Curso>, ICursoRepository
    {
        public CursoRepository(UniversidadeContext context) : base(context)
        {
        }

        public async Task<Curso> BuscarCursoDepartamentoEMatriculasPorCursoId(int id)
        {
            return await _context.Set<Curso>()
                .Include(x => x.Matriculas).ThenInclude(x=> x.Estudante)
                .Include(x=> x.Departamento)
                .SingleOrDefaultAsync(x => x.CursoID == id);
        }
        public async Task<List<Curso>> ListarCursosDepartamentoEMatriculas()
        {
            return await _context.Set<Curso>()
                .Include(x => x.Matriculas)
                .Include(x => x.Departamento)
                .ToListAsync();
        }
    }
}

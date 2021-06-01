using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Universidade.Core.Entidades;
using Universidade.Core.Interfaces;
using Universidade.Infrastructure.Data;

namespace Universidade.Infrastructure.Repositorio
{
    public abstract class RepositorioGenerico<T> : IRepository<T> where T : EntidadeBase
    {
        protected readonly UniversidadeContext _context;
        public RepositorioGenerico(UniversidadeContext context)
        {
            _context = context;
        }

        public async Task<T> BuscarPorId(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> Listar()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async void Adicionar(T objeto)
        {
            await _context.Set<T>().AddAsync(objeto);
        }

        public void Deletar(int id)
        {
            var objeto = _context.Set<T>().FindAsync(id).Result;
            _context.Set<T>().Remove(objeto);
        }

        public void Editar(T objeto)
        {
            _context.Set<T>().Update(objeto);
        }

        public async Task<IEnumerable<T>> Listar(Expression<Func<T, bool>> filtro)
        {
            return await _context.Set<T>().Where(filtro).ToListAsync();
        }
    }
}

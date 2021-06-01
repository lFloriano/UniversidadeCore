using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Universidade.Core.Entidades;

namespace Universidade.Core.Interfaces
{
    public interface IRepository<T> where T : EntidadeBase
    {
        Task<T> BuscarPorId(int id);
        Task<IEnumerable<T>> Listar();
        Task<IEnumerable<T>> Listar(Expression<Func<T, bool>> filtro);
        void Adicionar(T objeto);
        void Deletar(int id);
        void Editar(T objeto);
    }
}

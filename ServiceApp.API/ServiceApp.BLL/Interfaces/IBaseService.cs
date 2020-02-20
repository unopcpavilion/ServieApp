using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServiceApp.BLL.Interfaces
{
   public interface IBaseService<T, TId> where T: class
    {
		IQueryable<T> GetAll(out int count, Expression<Func<T, bool>> func);
		IQueryable<T> GetAll(Expression<Func<T, bool>> func);
		IQueryable<T> GetAll();

		Task<T> Get(TId id);
		Task<T> Create(T data);
		Task<T> Update(T data);
		Task Delete(T data);
		Task Delete(TId id);
	}
}

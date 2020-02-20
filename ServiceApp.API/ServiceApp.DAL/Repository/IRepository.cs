using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ServiceApp.DAL.Models;

namespace ServiceApp.DAL.Repository
{
    public interface IRepository<TEntity, TId> where TEntity: class
    {

		IDbContextTransaction BeginTransaction();
		void CommitTransaction();
		void RollbackTransaction();
		void DisposeTransaction();
		Task<TEntity> Create(TEntity entity);
		Task<int> CreateMany(IEnumerable<TEntity> entity);
		Task<int> RemoveMany(IEnumerable<TEntity> entity);

		Task<TEntity> Update(TEntity entity);

		Task<bool> UpdateRange(IEnumerable<TEntity> entities);

		Task Delete(TId id);

		Task Delete(TEntity id);

		Task<TEntity> GetById(TId id);

		IQueryable<TEntity> GetList(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = default(int?),
			int? take = default(int?),
			bool asNoTracking = true,
			bool notDeleted = true);

		IQueryable<TEntity> GetQuery(
			Expression<Func<TEntity, bool>> filter = null,
			string includeProperties = null);

		IQueryable<TEntity> GetQueryable(
			out int count,
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = null,
			int? skip = default(int?),
			int? take = default(int?),
			bool asNoTracking = true,
			bool notDeleted = true,
			bool calculateCount = false);




	}
}

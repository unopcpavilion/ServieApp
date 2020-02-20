using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ServiceApp.DAL.Models;
using ServiceApp.DAL.EFContext;

namespace ServiceApp.DAL.Repository
{
    public class BaseRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity  : class
    {
        protected virtual DbSet<TEntity> Entities => entities ?? dbContext.Set<TEntity>();
        private readonly ServiceContext dbContext;
        private readonly DbSet<TEntity> entities;
        public BaseRepository(ServiceContext serviceContext)
        {
            dbContext = serviceContext;
        }
        public IDbContextTransaction BeginTransaction()
        {
            return dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            dbContext.Database.CommitTransaction();
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var item = await dbContext.Set<TEntity>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<int> CreateMany(IEnumerable<TEntity> entity)
        {
            await dbContext.Set<TEntity>().AddRangeAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity data)
        {
            dbContext.Set<TEntity>().Remove(data);
            await dbContext.SaveChangesAsync();
        }
        public async Task Delete(TId id)
        {
            dbContext.Set<TEntity>().Remove(await GetById(id));
            await dbContext.SaveChangesAsync();
        }

        public void DisposeTransaction()
        {
            dbContext.Database.CurrentTransaction.Dispose();
        }

        public async Task<TEntity> GetById(TId id)
        {
            return await this.Entities.FindAsync(id);
        }     

        public async Task<int> RemoveMany(IEnumerable<TEntity> entity)
        {
            dbContext.Set<TEntity>().RemoveRange(entity);
            return await dbContext.SaveChangesAsync();
        }

        public void RollbackTransaction()
        {
            dbContext.Database.RollbackTransaction();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var item = dbContext.Set<TEntity>().Update(entity);
           await dbContext.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<bool> UpdateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                dbContext.Set<TEntity>().UpdateRange(entities);
                await  dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual IQueryable<TEntity> GetList(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = null,
           int? skip = default(int?),
           int? take = default(int?),
           bool asNoTracking = true,
           bool notDeleted = true)
        {
            int count;
            var query = GetQueryable(out count, filter, orderBy, includeProperties, skip, take, asNoTracking, notDeleted, true);

            return query;
        }

        public IQueryable<TEntity> GetQueryable(
            out int count,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = default(int?),
            int? take = default(int?),
            bool asNoTracking = true,
            bool notDeleted = true,
            bool calculateCount = false)
        {
            var query = CreateQuery(filter, includeProperties);

            //if (notDeleted)
            //  query = query.Where(x => !x.Deleted);

            if (calculateCount)
                count = query.Count();
            else
                count = 0;

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (asNoTracking)
                query = query.AsNoTracking();

            return query;
        }

        public virtual IQueryable<TEntity> GetQuery(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
        {
            return CreateQuery(filter, includeProperties);
        }

        protected IQueryable<TEntity> CreateQuery(
            Expression<Func<TEntity, bool>> filter = null,
            string Includes = null)
        {
            var query = this.Entities.AsQueryable();

            if (!String.IsNullOrEmpty(Includes))
                foreach (var property in Includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(property);

            if (filter != null)
                query = query.Where(filter);

            return query;
        }
    }
}

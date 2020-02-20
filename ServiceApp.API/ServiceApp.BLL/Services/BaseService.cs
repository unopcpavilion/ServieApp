using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


using ServiceApp.BLL.Interfaces;
using ServiceApp.DAL.Repository;

namespace ServiceApp.BLL.Services
{
   public class BaseService<T, TId> : IBaseService<T, TId> where T : class
    {
        protected readonly IRepository<T, TId> _repository;
        
        public BaseService(IRepository<T, TId> repository)
        {
            _repository = repository;
        }
        public virtual IQueryable<T> GetAll(out int count, Expression<Func<T, bool>> func) =>
           _repository.GetQueryable(out count, func, calculateCount: true);

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> func) => _repository.GetQueryable(out int count, func);

        public virtual IQueryable<T> GetAll() => _repository.GetQuery();
        public virtual async Task<T> Update(T data) => await _repository.Update(data);

        public virtual async Task<bool> UpdateRange(IEnumerable<T> data) => await _repository.UpdateRange(data);

        public virtual async Task<T> Create(T data) => await _repository.Create(data);
        public virtual async Task<int> CreateMany(IEnumerable<T> data) => await _repository.CreateMany(data);
        public virtual async Task<int> RemoveMany(IEnumerable<T> data) => await _repository.RemoveMany(data);

        public virtual async Task<T> Get(TId id) => await _repository.GetById(id);

        public virtual async Task Delete(T data) => await _repository.Delete(data);

        public virtual async Task Delete(TId id) => await _repository.Delete(id);
    }
}


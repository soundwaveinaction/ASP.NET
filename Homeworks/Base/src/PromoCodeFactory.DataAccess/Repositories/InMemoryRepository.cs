using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<T> CreateAsync(T entity)
        {
            var tempData = Data.ToList();
            tempData.Add(entity);
            Data = tempData;
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == entity.Id));
        }
        public Task<T> DeleteAsync(T entity)
        {
            var tempData = Data.ToList();
            tempData.Remove(entity);
            Data = tempData;
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == entity.Id));
        }
        public Task<T> UpdateAsync(T entity)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == entity.Id));
        }
    }
}
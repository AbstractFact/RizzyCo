using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Services
{
    public interface IService<T> where T : class, IEntity
    {
        public Task<List<T>> GetAll();
        public Task<T> Get(int id);
        public Task<T> Put(T entity);
        public Task<T> Post(T entity);
        public Task<T> Delete(int id);
    }
}

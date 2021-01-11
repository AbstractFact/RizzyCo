using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IService<T> where T : class
    {
        public Task<List<T>> GetAll();
        public Task<T> Get(int id);
        public T Put(T entity);
        public Task<T> Post(T entity);
        public T Delete(int id);
    }
}

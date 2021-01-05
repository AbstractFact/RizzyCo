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
    }
}

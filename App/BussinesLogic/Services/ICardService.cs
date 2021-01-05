using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace BussinesLogic.Services
{
    public interface ICardService
    {
        public Task<List<Card>> GetAllCards();
    }
}

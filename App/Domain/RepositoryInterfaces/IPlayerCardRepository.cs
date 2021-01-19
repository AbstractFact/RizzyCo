using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IPlayerCardRepository: IRepository<PlayerCard>
    {
       Task<List<PlayerCard>> GetPlayerCards(int playerID);
    }
}

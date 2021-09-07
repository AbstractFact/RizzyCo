using DataAccess.Models;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game> NextStage(int gameID);
    }
}

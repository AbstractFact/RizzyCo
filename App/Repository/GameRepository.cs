using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;

namespace Repository
{
    public class GameRepository : Repository<Game, RizzyCoContext>, IGameRepository
    {
        public GameRepository(RizzyCoContext context) : base(context)
        {
           
        }
        public async Task<Game> NextStage(int gameID)
        {
            Game game = await context.Games.FindAsync(gameID);
            game.Stage++;
            context.Games.Update(game);
            context.SaveChanges();
            return game;
        }

    }
}

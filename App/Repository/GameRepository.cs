using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

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

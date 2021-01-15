using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GameUserRepository : Repository<GameUser, RizzyCoContext>, IGameUserRepository
    {
        public GameUserRepository(RizzyCoContext context) : base(context)
        {

        }

        public async Task<List<GameUser>> GetAllUserGames(User user)
        {

            return await context.Set<GameUser>().Include(p=>p.Game).Where(p => p.User == user).ToListAsync();
        }

        public async Task<GameUser> AddGameUser(Game game, User user)
        {
            GameUser entity = new GameUser();
            entity.Game = game;
            entity.User = user;

            await context.Set<GameUser>().AddAsync(entity);
            return entity;
        }
    }
}

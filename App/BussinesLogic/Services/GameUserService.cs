using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;


namespace BussinesLogic.Services
{
    public class GameUserService : IGameUserService
    {
        private readonly IUnitOfWork unit;

        public GameUserService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<GameUser>> GetAllUserGames(int userID)
        {
            using (unit)
            {
                Task<User> uu = unit.Users.Get(userID);
                User user = await uu;
                Task<List<GameUser>> games = unit.GamesUser.GetAllUserGames(user);

                return await games;
            }
        }

    }
}

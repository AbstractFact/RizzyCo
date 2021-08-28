using DataAccess.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Services.Strategy
{
    public class ContinentStrategy : IMissionStrategy
    {
        private readonly IUnitOfWork unit;
        private string continent1;
        private string continent2;
        private bool continent3;

        private int mapID;
        public ContinentStrategy(IUnitOfWork unit, string continent1, string continent2, bool continent3, int mapID)
        {
            this.unit = unit;
            this.continent1 = continent1;
            this.continent2 = continent2;
            this.continent3 = continent3;
            this.mapID = mapID;
        }
        public async Task<bool> CheckComplete(int playerID)
        {
            int res = 0;
            List<PlayerTerritory> playerTerritories = await unit.PlayerTerritories.GetPlayerTerritories(playerID);
            List<Territory> territories = new List<Territory>();
            playerTerritories.ForEach(pt =>
            {
                territories.Add(pt.Territory);
            });

            List<Territory> continent1Territories = await unit.Territories.GetContinentTerritoriesByName(continent1);
            if (continent1Territories.Intersect(territories).Count() == continent1Territories.Count())
                res++;

            List<Territory> continent2Territories = await unit.Territories.GetContinentTerritoriesByName(continent2);
            if (continent2Territories.Intersect(territories).Count() == continent2Territories.Count())
                res++;

            if(continent3)
            {

                List<Continent> continents = await unit.Continents.GetMapContinents(mapID);

                foreach (Continent c in continents)
                {
                    List<Territory> continentTerritories = await unit.Territories.GetContinentTerritories(c.ID);
                    if (continentTerritories.Intersect(territories).Count() == continentTerritories.Count())
                    {
                        if (c.Name != continent1 && c.Name != continent2)
                            res++;
                    }
                }
            }

            if (continent3)
                return res >= 3;
            else
                return res >= 2;

        }
    }
}

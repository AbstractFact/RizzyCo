using BussinesLogic.Messaging;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using DTOs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinesLogic.Services
{
    public class PlayerTerritoryService : IPlayerTerritoryService
    {
        private readonly IUnitOfWork unit;
        private HubService hubService;

        public PlayerTerritoryService(IUnitOfWork unit, IHubContext<MessageHub> hubContext)
        {
            this.unit = unit;
            hubService = new HubService(hubContext);
        }

        public PlayerTerritory Delete(int id)
        {
            using (unit)
            {
                PlayerTerritory playerTerritory = unit.PlayerTerritories.Delete(id);

                unit.Complete();

                return playerTerritory;
            }
        }

        public async Task<PlayerTerritory> Get(int id)
        {
            using (unit)
            {
                Task<PlayerTerritory> playerTerritory = unit.PlayerTerritories.Get(id);

                if (playerTerritory == null) return null;

                return await playerTerritory;
            }
        }

        public async Task<List<PlayerTerritory>> GetAll()
        {
            using (unit)
            {
                Task<List<PlayerTerritory>> playerTerritories = unit.PlayerTerritories.GetAll();

                return await playerTerritories;
            }
        }

        public async Task<PlayerTerritory> Post(PlayerTerritory entity)
        {
            using (unit)
            {
                Task<PlayerTerritory> playerTerritory = unit.PlayerTerritories.Add(entity);

                unit.Complete();

                return await playerTerritory;
            }
        }

        public PlayerTerritory Put(PlayerTerritory entity)
        {
            using (unit)
            {
                PlayerTerritory playerTerritory = unit.PlayerTerritories.Update(entity);

                unit.Complete();

                return playerTerritory;
            }
        }

        public async Task<List<PlayerTerritoryDTO>> GetPlayerTerritories(int playerID)
        {
            using (unit)
            {
                List<PlayerTerritory> territories = await unit.PlayerTerritories.GetPlayerTerritories(playerID);
                List<PlayerTerritoryDTO> territoriesDTO = new List<PlayerTerritoryDTO>();

                territories.ForEach(element =>
                {
                    territoriesDTO.Add(new PlayerTerritoryDTO(element));

                });

                return territoriesDTO;
            }
        }


        public async Task<AddArmieDTO> AddArmie(int gameID, int playerID, int territoryID)
        {
            using (unit)
            {
                PlayerTerritory playerTerritory = await unit.PlayerTerritories.AddArmie(playerID, territoryID);
                await unit.Players.UpdateAvailableArmies(playerID);
                string nextPlayer = await unit.Players.EndTurn(gameID);
                unit.Complete();

                return new AddArmieDTO { TerritoryID = territoryID, NumArmies = playerTerritory.Armies, NextPlayer = nextPlayer };
            }
        }

        public async Task<AddArmieDTO> AddReinforcement(AddReinforcementDTO dto)
        {
            using (unit)
            {
                PlayerTerritory playerTerritory = await unit.PlayerTerritories.AddReinforcement(dto.PlayerID, dto.TerritoryID, dto.NumArmies);
                await unit.Players.UpdateAvailableReinforcements(dto.PlayerID, -dto.NumArmies);
                unit.Complete();

                return new AddArmieDTO { TerritoryID = dto.TerritoryID, NumArmies = playerTerritory.Armies };
            }
        }

        public async Task<AttackInfoDTO> Attack(AttackDTO dto)
        {
            using (unit)
            {
                PlayerTerritory targetPlayer = await unit.PlayerTerritories.GetPlayer(dto.TargetID, dto.GameID);
                return new AttackInfoDTO { PlayerAttackedID = dto.PlayerID, PlayerAttackedName = dto.PlayerUsername,  NumDice = dto.NumDice, TargetPlayer = targetPlayer.Player.User.Username, AttackFromTerritory = dto.AttackFromID, AttackFromTerritoryName=dto.AttackFromName, TargetTerritory = dto.TargetID, TargetTerritoryName = targetPlayer.Territory.Name };
            }
        }

        public async Task<ThrowDiceNotificationDTO> ThrowDice(ThrowDiceDTO dto)
        {
            bool winner = false;
            int armiesLost1 = 0;
            int armiesLost2 = 0;

            List<int> diceValues1 = new List<int>(dto.NumDice1);
            List<int> diceValues2 = new List<int>(dto.NumDice2);

            GenerateDiceValues(diceValues1, dto.NumDice1, diceValues2, dto.NumDice2);
            int range = dto.NumDice1 < dto.NumDice2 ? dto.NumDice1 : dto.NumDice2;
            for (int i = 0; i < range; i++)
                if (diceValues1[i] > diceValues2[i])
                    armiesLost2++;
                else
                    armiesLost1++;

            using (unit)
            {
                PlayerTerritory playerTerritory1 = await unit.PlayerTerritories.GetPlayer(dto.Territory1ID, dto.GameID);
                PlayerTerritory playerTerritory2 = await unit.PlayerTerritories.GetPlayer(dto.Territory2ID, dto.GameID);

                playerTerritory1.Armies -= armiesLost1;
                playerTerritory2.Armies -= armiesLost2;

                if (playerTerritory2.Armies == 0)
                {
                    playerTerritory2.Player = playerTerritory1.Player;
                    playerTerritory2.Armies = 1;
                    playerTerritory1.Armies--;
                    winner = true;
                }
                unit.PlayerTerritories.Update(playerTerritory1);
                unit.PlayerTerritories.Update(playerTerritory2);
                unit.Complete();
                return new ThrowDiceNotificationDTO { DiceValues1 = diceValues1,
                                                      DiceValues2 = diceValues2,
                                                      Winner = winner,
                                                      Player1ID = dto.Player1ID,
                                                      Player1Color = playerTerritory1.Player.PlayerColor.Value,
                                                      Player2ID = dto.Player2ID, 
                                                      Territory1ID = playerTerritory1.Territory.ID,
                                                      Territory2ID=playerTerritory2.Territory.ID,
                                                      NumArmies1=playerTerritory1.Armies, 
                                                      NumArmies2=playerTerritory2.Armies 
                                                    };
            }

            
        }

        public void GenerateDiceValues(List<int> diceValues1, int numDice1, List<int> diceValues2, int numDice2)
        {
            Random random = new Random();

            for (int i = 0; i < numDice1; i++)
                diceValues1.Add(random.Next(1, 6));

            for (int i = 0; i < numDice2; i++)
                diceValues2.Add(random.Next(1, 6));

            diceValues1.Sort((a, b) => b.CompareTo(a));
            diceValues2.Sort((a, b) => b.CompareTo(a));
        }

        public async Task<TransferArmiesDTO> Transfer(TransferArmiesDTO dto)
        {
            using(unit)
            {
                PlayerTerritory playerTerritory1 = await unit.PlayerTerritories.GetPlayer(dto.TerrFromID, dto.GameID);
                PlayerTerritory playerTerritory2 = await unit.PlayerTerritories.GetPlayer(dto.TerrToID, dto.GameID);

                playerTerritory1.Armies -= dto.NumArmies;
                playerTerritory2.Armies += dto.NumArmies;

                unit.PlayerTerritories.Update(playerTerritory1);
                unit.PlayerTerritories.Update(playerTerritory2);
                unit.Complete();

                return dto;
            }

        }

    }
}

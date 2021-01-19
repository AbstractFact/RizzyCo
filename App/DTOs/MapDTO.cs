using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class MapDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumberOfContinents { get; set; }
        public int NumberOfTerritories { get; set; }
        public int NumberOfAvailableArmies { get; set; }

        public MapDTO() { }
        public MapDTO(Map map)
        {
            ID = map.ID;
            Name = map.Name;
            NumberOfContinents = map.NumberOfContinents;
            NumberOfTerritories = map.NumberOfTerritories;
            NumberOfAvailableArmies = map.NumberOfAvailableArmies;
        }

        public static Map FromDTO(MapDTO dto)
        {
            Map c = new Map()
            {
                ID = dto.ID,
                Name = dto.Name,
                NumberOfContinents = dto.NumberOfContinents,
                NumberOfTerritories = dto.NumberOfTerritories,
                NumberOfAvailableArmies = dto.NumberOfAvailableArmies
            };
            return c;
        }

        public static List<MapDTO> FromEntityList(List<Map> list)
        {
            List<MapDTO> dtoList = new List<MapDTO>();
            foreach (var c in list)
            {
                if (c != null)
                {
                    dtoList.Add(new MapDTO(c));
                }
            }

            return dtoList;
        }

    }
}

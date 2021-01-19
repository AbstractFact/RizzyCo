using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class PlayerColorDTO
    {
        public int ID { get; set; }
        public string Value { get; set; }

        public PlayerColorDTO() { }
        public PlayerColorDTO(PlayerColor playerColor)
        {
            ID = playerColor.ID;
            Value = playerColor.Value;
        }

        public static PlayerColor FromDTO(PlayerColorDTO dto)
        {
            PlayerColor c = new PlayerColor()
            {
                ID = dto.ID,
                Value = dto.Value     
            };
            return c;
        }

        public static List<PlayerColorDTO> FromEntityList(List<PlayerColor> list)
        {
            List<PlayerColorDTO> dtoList = new List<PlayerColorDTO>();
            foreach (var c in list)
            {
                if (c != null)
                {
                    dtoList.Add(new PlayerColorDTO(c));
                }
            }

            return dtoList;
        }

       
    }
}

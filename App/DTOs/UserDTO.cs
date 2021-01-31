using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public UserDTO() { }

        public UserDTO(User user)
        {
            ID = user.ID;
            Username = user.Username;
            Email = user.Email;
            Password = user.Password;
            Role = user.Role;
            Token = user.Token;
        }

        public static User FromDTO(UserDTO dto)
        {
            User c = new User()
            {
                ID = dto.ID,
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
                Token = dto.Token
            };
            return c;
        }

        public static List<UserDTO> FromEntityList(List<User> list)
        {
            List<UserDTO> dtoList = new List<UserDTO>();
            foreach (var c in list)
            {
                if (c != null)
                {
                    dtoList.Add(new UserDTO(c));
                }
            }

            return dtoList;
        }
    }

    public class CreateUserDTO
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public CreateUserDTO() { }

        public static User FromDTO(CreateUserDTO dto)
        {
            User c = new User()
            {
                ID = dto.ID,
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
            };
            return c;
        }
    }

    public class UpdateUserDTO
    {
        public int ID { get; set; }
        public String Password { get; set; }

        public static User FromDTO(UpdateUserDTO dto)
        {
            User c = new User()
            {
                Password = dto.Password
            };
            return c;
        }
    }
}

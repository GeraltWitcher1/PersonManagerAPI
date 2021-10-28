using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using PersonManagerAPI.Models;

namespace PersonManagerAPI.Data
{
    public class UserManager : IUserService
    {
        private List<User> users;
        private const string usersFile = "users.json";

        public UserManager()
        {
            if (!File.Exists(usersFile))
            {
                GenerateDefault();
                WriteUsersToJson();
            }
            else
            {
                string content = File.ReadAllText(usersFile);
                users = JsonSerializer.Deserialize<List<User>>(content);
            }
        }

        private void GenerateDefault()
        {
            users = new[]
            {
                new User
                {
                    Username = "admin",
                    Password = "admin",
                    Role = "Admin"
                },
                new User
                {
                    Username = "Deniss",
                    Password = "coolpassword123",
                    Role = "User"
                }
            }.ToList();
        }

        private void WriteUsersToJson()
        {
            string todosAsJson = JsonSerializer.Serialize(users);
            File.WriteAllText(usersFile, todosAsJson);
        }


        public async Task<User> ValidateUser(string username, string password)
        {
            User first = users.FirstOrDefault(user => user.Username.Equals(username));

            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }
    }
}
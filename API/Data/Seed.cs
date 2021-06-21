using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Data
{
    public static class Seed
    {
        public static async Task SeedUser(DataContext dataContext) 
        {
            if (await dataContext.Users.AnyAsync()) return;

            var seedData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<AppUser>>(seedData);

            foreach (var user in users) 
            {
                using var rmacsha = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = rmacsha.ComputeHash(Encoding.UTF8.GetBytes("pa$$w0rd"));
                user.PasswordKey = rmacsha.Key;
                dataContext.Users.Add(user);
            }
            await dataContext.SaveChangesAsync();
        }
    }
}

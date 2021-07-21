using API.DTOs;
using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser appUser);

        Task<bool> SaveAsAsync();

        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUserNameAsync(string username);

        Task<MembersDto> GetMemberDtoAsync(string username);

        Task<IEnumerable<MembersDto>> GetMembersDtoAsync();
    }
}

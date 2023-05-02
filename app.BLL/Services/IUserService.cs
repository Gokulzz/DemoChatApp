using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using app.BLL.DTO;
using app.DAL.Model;
using app.DAL.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.Metadata;

namespace app.BLL.Services
{
    public interface IUserService
    {
        Task<ApiResponse> GetUserById(int id);
        Task<ApiResponse> GetUsers();
        Task<ApiResponse> AddUser(UserDTO user);
        Task<ApiResponse> UpdateUser(UserDTO user);
        Task<ApiResponse> DeleteUser(int id);
        Task<ApiResponse> LoginUser(UserLoginDTO userLogin);
        Task<ApiResponse> PatchUpdateUser(int id, JsonPatchDocument patchDocument);


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using app.BLL.DTO;

namespace app.BLL.Services
{
    public interface IUserRoleService
    {
        Task<ApiResponse> GetRoleById(int id);
        Task<ApiResponse> GetRoles();
        Task<ApiResponse> AddRoles(UserRoleDTO userRoleDTO);
        Task<ApiResponse> UpdateRoles(UserRoleDTO userRoleDTO);
        Task<ApiResponse> DeleteRoles(int id);
    }
}

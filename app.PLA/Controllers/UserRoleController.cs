using app.BLL.DTO;
using app.BLL.Implementations;
using app.BLL;
using app.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace app.PLA.Controllers
{
    public class UserRoleController : Controller
    {
        public IUserRoleService userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            this.userRoleService = userRoleService;
        }
        [HttpGet("GetUserRole")]
        public async Task<ApiResponse> GetUserRole(int id)
        {
            var getRole = await userRoleService.GetRoleById(id);
            return getRole;
        }
        [HttpGet("GetAllRoles")]
        public async Task<ApiResponse> GetAllUser()
        {
            var getRoles = await userRoleService.GetRoles();
            return getRoles;
        }
        [HttpPost("AddRoles")]
        public async Task<ApiResponse> AddRoles(UserRoleDTO userRole)
        {
            var addUserRoles = await userRoleService.AddRoles(userRole);
            return addUserRoles;
        }
        [HttpPut("UpdateUserRoles")]
        public async Task<ApiResponse> UpdateUserRoles(UserRoleDTO user)
        {
            var update_UserRoles = await userRoleService.UpdateRoles(user);
            return update_UserRoles;
        }
        [HttpDelete("DeleteUserRoles")]
        public async Task<ApiResponse> DeleteUserRoles(int id)
        {
            var delete_UserRoles = await userRoleService.DeleteRoles(id);
            return delete_UserRoles;
        }
    }
}

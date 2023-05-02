using app.BLL;
using app.BLL.DTO;
using app.BLL.Services;
using app.DAL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace app.PLA.Controllers
{

    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
        public IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet("GetUser")]
        public async Task<ApiResponse> GetUser(int id)
        {
            var getUser = await userService.GetUserById(id);
            return getUser;
        }
        [HttpGet("GetAllUser")]
        public async Task<ApiResponse> GetAllUser()
        {
            var getUsers = await userService.GetUsers();
            return getUsers;
        }
        [HttpPost("AddUser")]
        public async Task<ApiResponse> AddUsers(UserDTO user)
        {
            var addUser = await userService.AddUser(user);
            return addUser;
        }
        [HttpPut("UpdateUsers")]
        public async Task<ApiResponse> UpdateUsers(UserDTO user)
        {
            var update_User = await userService.UpdateUser(user);
            return update_User;
        }
        [HttpDelete("DeleteUsers")]
        public async Task<ApiResponse> DeleteUsers(int id)
        {
            var delete_User = await userService.DeleteUser(id);
            return delete_User;
        }
        [HttpPatch("UpdateUserPatch")]
        public async Task<ApiResponse> UpdateUserPatch(int id,[FromBody] JsonPatchDocument patch)
        {
            var update_user = await userService.PatchUpdateUser(id, patch); 
            return update_user; 
        }
    }
       
    
}


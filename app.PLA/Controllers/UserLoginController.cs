using app.BLL;
using app.BLL.DTO;
using app.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace app.PLA.Controllers
{
    public class UserLoginController : Controller
    {
        public IUserService userService;
        public UserLoginController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("Login")]
        public async Task<ApiResponse> Login(UserLoginDTO userLogin)
        {
            var user = await userService.LoginUser(userLogin);
            return user;
        }
    }
}

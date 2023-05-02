using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.DAL.Model;

namespace app.BLL.DTO
{
    public class UserDTO
    {
        public int userID { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }   
        public string VerificationToken { get; set; }
        public int RoleId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.DAL.Model
{
    public class User
    {
        [Key]
        public int userId { get; set; } 
        public string UserName { get; set; }    
        public string Email { get; set; }   
        public byte[]  PasswordHash { get; set; }   
        public byte[] PasswordSalt { get; set; }    
        public string? VerificationToken { get; set; }  
        public DateTime? VerifiedAt { get; set; }   
        public UserRole Role { get; set; }  
        public int RoleID { get; set; } 
        public string? RoleName { get; set; }
    }
}

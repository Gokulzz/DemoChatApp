using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace app.DAL.Model
{
    public class UserRole
    {
        public int roleId { get; set; } 
        public string roleName { get; set; }
        public string roleDescription { get; set; }
        public ICollection<User> Users { get; set; }    

    }
}

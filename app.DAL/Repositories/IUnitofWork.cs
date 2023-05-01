using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using app.DAL.Model;

namespace app.DAL.Repositories
{
    public interface IUnitofWork
    {
        public IUserRepository userRepository { get; }
        public IUserRoleRepository userRoleRepository { get; }  
        public IChatBoxRepository chatBoxRepository { get; }
        Task Save();
        Task<User> FindByUserEmail(string email);
       
    }
}

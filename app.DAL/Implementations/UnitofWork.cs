using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.DAL.Model;
using app.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace app.DAL.Implementations
{
    public class UnitofWork : IUnitofWork 
    {
        public IUserRoleRepository userRoleRepository { get; }
        public IUserRepository userRepository { get; }
        public IChatBoxRepository chatBoxRepository { get; }
        public readonly DataContext dataContext;
        public UnitofWork(DataContext dataContext) 
        { 
            this.dataContext = dataContext; 
            userRepository = new UserRepository(dataContext);
            userRoleRepository= new UserRoleRepository(dataContext);  
            chatBoxRepository= new ChatBoxRepository(dataContext);

        } 
        public async Task Save()
        {
            await dataContext.SaveChangesAsync();
        }
        public async Task<User> FindByUserEmail(string email)
        {
            var user = await dataContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            var roles = await userRoleRepository.GetAsync(user.RoleID);
            user.RoleName = roles.roleName;
            return user;
        }
    }
 }


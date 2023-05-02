using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using app.DAL.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace app.DAL.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        public Task<User> FindByUserName(string userName); 
        public Task<User> UpdateUserPatch(int id, JsonPatchDocument patchDocument);    
        

    }
}

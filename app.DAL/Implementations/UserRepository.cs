﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using app.DAL.Model;
using app.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualBasic;

namespace app.DAL.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {

        } 
        public async Task<User> FindByUserName(string userName)
        {
            var search_Name = await context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            return search_Name;
        }
       
            
        
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.BLL.Implementations;
using app.BLL.Services;
using app.BLL.Validation;
using app.DAL;
using app.DAL.Implementations;
using app.DAL.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigureManager.cs
{
    public static class ConfigureDependencies
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<DataContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IChatBoxRepository, ChatBoxRepository>();
            services.AddScoped<IUnitofWork, UnitofWork>();  
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleService, UserRoleService>();    
            services.AddScoped<IChatBoxService, ChatBoxService>();  
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
           
            
        }
    }
}

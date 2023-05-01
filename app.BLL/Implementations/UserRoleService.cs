using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using app.BLL.DTO;
using app.BLL.Exceptions;
using app.BLL.Services;
using app.DAL.Model;
using app.DAL.Repositories;
using AutoMapper;

namespace app.BLL.Implementations
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitofWork unitofWork;
        public IMapper mapper;
        public UserRoleService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }   
        public async Task<ApiResponse> GetRoleById(int id)
        {
            var role = await unitofWork.userRoleRepository.GetAsync(id);
            if(role==null)
            {
                throw new NotFoundException($"Role of this id= {id} could not be found\n");
            }
            return new ApiResponse(200, "role displayed successfully", role);
        }
        public async Task<ApiResponse> GetRoles()
        {
            var roles = await unitofWork.userRoleRepository.GetAllAsync();
            if(roles==null)
            {
                throw new NotFoundException("Roles could not be found");
            }
            return new ApiResponse(200, "roles displayed successfully", roles);
        }
        public async Task<ApiResponse> AddRoles(UserRoleDTO userRole)
        {
            try
            {
                var mapRoles = mapper.Map<UserRole>(userRole);
                await unitofWork.userRoleRepository.Post(mapRoles);
                await unitofWork.Save();
                return new ApiResponse(200, "Roles added successfully", mapRoles);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
        public  async Task<ApiResponse> UpdateRoles(UserRoleDTO userRole)
        {
            throw new NotImplementedException();

        }
        public async Task<ApiResponse> DeleteRoles(int id)
        {
            var roles = await unitofWork.userRoleRepository.Delete(id);
            if(roles==null)
            {
                throw new NotFoundException($"Role of this {id} does not exist");
            }
            return new ApiResponse(200, "Role deleted successfully", roles);
            
        }
    }
}

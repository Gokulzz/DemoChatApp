using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.BLL.DTO;
using app.DAL.Model;
using AutoMapper;

namespace app.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<UserDTO, User>();
            CreateMap<UserRoleDTO, UserRole>();
            CreateMap<ChatBoxDTO, ChatBox>()
                .ForMember(dest => dest.TimeStamp, c => c.MapFrom(src => DateTime.Now));
         
                
        }
    }
}

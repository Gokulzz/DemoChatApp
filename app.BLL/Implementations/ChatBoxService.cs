using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using app.BLL.DTO;
using app.BLL.Exceptions;
using app.BLL.Services;
using app.DAL.Model;
using app.DAL.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace app.BLL.Implementations
{
    public class ChatBoxService: IChatBoxService
    {
        private readonly IUnitofWork unitofWork;
        public IMapper mapper;
       
        public ChatBoxService(IUnitofWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }   
        public async Task<ApiResponse> GetMessageById(int id)
        {
            var message = await unitofWork.chatBoxRepository.GetAsync(id);
            if(message==null)
            {
                throw new NotFoundException($"Message of this {id} could not be found");
            }
            return new ApiResponse(200, "message displayed successfully", message);
        }
        public async Task<ApiResponse>  SendChat(ChatBoxDTO chatBoxDTO)
        {
            try
            {
                var search_Sender = await unitofWork.userRepository.FindByUserName(chatBoxDTO.Sender);
                var search_Receiver = await unitofWork.userRepository.FindByUserName(chatBoxDTO.Receiver);
                if(search_Sender==null || search_Receiver==null)
                {
                    throw new System.UnauthorizedAccessException();
                }
                var chat = mapper.Map<ChatBox>(chatBoxDTO);
                await unitofWork.chatBoxRepository.Post(chat);
                await unitofWork.Save();
                return new ApiResponse(200, "message sent successfully", chat);
            }
            catch(Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
           
        }
        public async Task<ApiResponse> UpdateChat(ChatBoxDTO chatBoxDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResponse> DeleteChat(int id)
        {
            try
            {
                var message = await unitofWork.chatBoxRepository.Delete(id);
                return new ApiResponse(200, "message deleted successufully", message);
            }
            catch(Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}

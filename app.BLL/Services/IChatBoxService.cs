using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.BLL.DTO;
using app.DAL.Model;

namespace app.BLL.Services
{
    public interface IChatBoxService
    {
        Task<ApiResponse> GetMessageById(int id);
        Task<ApiResponse> SendChat(ChatBoxDTO chatBox);
        Task<ApiResponse> UpdateChat(ChatBoxDTO chatBox);
        Task<ApiResponse> DeleteChat(int id);
    }
}

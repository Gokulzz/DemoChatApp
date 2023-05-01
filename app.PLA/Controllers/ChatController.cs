using app.BLL;
using app.BLL.DTO;
using app.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace app.PLA.Controllers
{
    public class ChatController : Controller
    {
        public IChatBoxService chatBoxService;
        private readonly IHubContext<ChatHub> hubContext;
        private readonly ILogger<ChatHub> logger;

        public ChatController(IChatBoxService chatBoxService, IHubContext<ChatHub> hubContext, ILogger<ChatHub> logger)
        {
            this.chatBoxService = chatBoxService;
            this.hubContext = hubContext;
            this.logger = logger;
        }
        [HttpGet("GetChat")]
        public async Task<ApiResponse> GetChat(int id)
        {
            var message= await chatBoxService.GetMessageById(id);
            return message;
        }
        [HttpPost("SendMessage")]
        public async Task<ApiResponse> SendMessage(ChatBoxDTO chatBox)
        {
            var chatMessage = await chatBoxService.SendChat(chatBox);
            logger.LogInformation($"Message received from {chatBox.Sender} : {chatBox.Message}");
            await hubContext.Clients.User(chatBox.Receiver).SendAsync("Receive Message", chatBox.Message);
            logger.LogInformation($"Mesage sent to the {chatBox.Receiver}");
            return chatMessage;
        }
        [HttpPut("UpdateMessage")]
        public async Task<ApiResponse> UpdateMessage(ChatBoxDTO chatBox)
        {
            var updateMessage = await chatBoxService.UpdateChat(chatBox);
            return updateMessage;
        }
        [HttpDelete("DeleteMessage")]
        public async Task<ApiResponse> DeleteMessage(int id)
        {
            var deleteMessage = await chatBoxService.DeleteChat(id);
            return deleteMessage;

        }
    }
}

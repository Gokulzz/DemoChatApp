using app.BLL.DTO;
using app.BLL.Services;
using Microsoft.AspNetCore.SignalR;

namespace app.PLA
{
    public class ChatHub : Hub
    {
        private readonly IChatBoxService chatBoxService;
        public ChatHub(IChatBoxService chatBoxService)
        {
            this.chatBoxService = chatBoxService; 
        }
        public async Task SendMessage(ChatBoxDTO chatBox)
        {
            
            await chatBoxService.SendChat(chatBox);
            await Clients.User(chatBox.Receiver).SendAsync("ReceiveMessage", chatBox.Message);
            
        }
    }
}
 
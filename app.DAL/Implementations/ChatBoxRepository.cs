using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.DAL.Model;
using app.DAL.Repositories;

namespace app.DAL.Implementations
{
    public class ChatBoxRepository : GenericRepository<ChatBox>, IChatBoxRepository 
    {
        public ChatBoxRepository(DataContext context) : base(context) 
        { 

        }
    }
}

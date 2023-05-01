﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.DAL.Model
{
    public class ChatBox
    {
        public int MessageId { get; set; }  
        public string Sender { get; set; }
        public string Receiver { get; set; }    
        public string Message { get; set; } 
        public DateTime TimeStamp { get; set; } 

    }
}
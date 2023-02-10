using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ChatModel
    {
        public List<ChatUser> Users;
        public List<ChatMessage> Messages;
        public ChatModel() 
        {
            Users = new List<ChatUser>();
            Messages = new List<ChatMessage>();

            Messages.Add(new ChatMessage() { Value = "Chat started " + DateTime.Now });
        }
    }

    public class ChatUser
    {
        public string Name { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LastPing { get; set; }
    }

    public class ChatMessage
    {
        public string Value { get; set; }
        public ChatUser User { get; set; }
        public DateTime Date = DateTime.Now;
    }
}
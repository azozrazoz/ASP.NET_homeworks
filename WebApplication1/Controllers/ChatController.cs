using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ChatController : Controller
    {
        public void logOFF(ChatUser user)
        {
            chatModel.Users.Remove(user);
            chatModel.Messages.Add(new ChatMessage()
            {
                Value = user.Name + " leaved from chat",
                Date = DateTime.Now,
            });
        }

        static ChatModel chatModel;

        public ActionResult Index(string user, bool? logOn, bool? logOff, string chatMessage)
        {
            try
            {
                if (chatModel == null)
                {
                    chatModel = new ChatModel();
                }

                if (chatModel.Messages.Count > 100)
                {
                    chatModel.Messages.RemoveRange(0, 90);
                }

                if (!Request.IsAjaxRequest())
                {
                    return View(chatModel);
                }
                else if (logOn != null && (bool)logOn)
                {
                    if (chatModel.Users.FirstOrDefault(u => u.Name == user) != null)
                    {
                        throw new Exception("This username is already taken");
                    }
                    else if (chatModel.Users.Count > 10)
                    {
                        throw new Exception("Limit in the chat (only 10 users)");
                    }
                    else
                    {
                        chatModel.Users.Add(new ChatUser()
                        {
                            Name = user,
                            LoginTime = DateTime.Now,
                            LastPing = DateTime.Now,
                        });

                        chatModel.Messages.Add(new ChatMessage()
                        {
                            Value = user + " joined!",
                            Date = DateTime.Now,
                        });
                    }
                    return PartialView("ChatRoom", chatModel);
                }
                else if (logOff != null && (bool)logOff)
                {
                    logOFF(chatModel.Users.FirstOrDefault(u => u.Name == user));
                    return PartialView("ChatRoom", chatModel);
                }
                else
                {
                    ChatUser currentUser = chatModel.Users.FirstOrDefault(u => u.Name == user);
                    currentUser.LastPing = DateTime.Now;

                    List<ChatUser> toRemove = new List<ChatUser>();
                    foreach (ChatUser user_ in chatModel.Users)
                    {
                        TimeSpan span = DateTime.Now - currentUser.LastPing;
                        if (span.TotalSeconds > 20)
                        {
                            toRemove.Add(user_);
                        }
                    }

                    foreach(ChatUser user_ in toRemove)
                    {
                        logOFF(user_);
                    }

                    if (!string.IsNullOrEmpty(chatMessage))
                    {
                        chatModel.Messages.Add(new ChatMessage()
                        {
                            User = currentUser,
                            Value = chatMessage,
                            Date = DateTime.Now,
                        });
                    }
                    return PartialView("History", chatModel);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }
    }
}
using System;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace WebsiteGamingGear.Others
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string time)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message, time);
        }
    }
}
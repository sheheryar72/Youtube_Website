using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Youtube_Website.Hubs
{
    public class CommentsHub : Hub
    {
        public void Hello()
        {
            Clients.All.someOtherMethod();
        }
        public void Comment(string userEmail, string comment)
        {
            Clients.All.newMessage(userEmail, comment);
        }
    }
}
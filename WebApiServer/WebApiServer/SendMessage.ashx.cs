using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiServer.Models;

namespace WebApiServer
{
    /// <summary>
    /// SendMessage 的摘要说明
    /// </summary>
    public class SendMessage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var clientID = context.Request.QueryString["clientID"];
            var message = context.Request.QueryString["message"];
            WebAsyncResultCollection.Instance.SendMessage(clientID, message);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
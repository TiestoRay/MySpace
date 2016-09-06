using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiServer.Models;

namespace WebApiServer
{
    /// <summary>
    /// NoticeHandler 的摘要说明
    /// </summary>
    public class NoticeHandler : IHttpAsyncHandler
    {

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            var clientID = context.Request.QueryString["id"];
            WebAsyncResultCollection.Instance.SendMessage(clientID, "ClearClientID");
            WebAsyncResult result = new WebAsyncResult(cb, context, clientID);
            WebAsyncResultCollection.Instance.Add(result);
            return result;
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            
        }


        public void ProcessRequest(HttpContext context)
        {

        }
    }
}
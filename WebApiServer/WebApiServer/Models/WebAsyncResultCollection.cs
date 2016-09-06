using System.Collections.Generic;
using System.Linq;

namespace WebApiServer.Models
{
    public class WebAsyncResultCollection : List<WebAsyncResult>, ICollection<WebAsyncResult>
    {
        private static WebAsyncResultCollection _instance = new WebAsyncResultCollection();

        public static WebAsyncResultCollection Instance
        {
            get { return WebAsyncResultCollection._instance; }
        }

        public bool SendMessage(string clientID, string message)
        {
            var result = this.FirstOrDefault(r => r.ClientID == clientID);
            if (result != null)
            {
                Remove(result);
                bool sendsuccess = false;
                if (result.Context.Response.IsClientConnected)
                {
                    sendsuccess = true;
                    result.Context.Response.Write(message);
                }
                result.SetComplete();
                return sendsuccess;
            }
            return false;
        }
    }
}
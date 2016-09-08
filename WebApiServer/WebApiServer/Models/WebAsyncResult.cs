using System;
using System.Threading;
using System.Web;

namespace WebApiServer.Models
{
    public class WebAsyncResult : IAsyncResult
    {
        private AsyncCallback _callback;

        public WebAsyncResult(AsyncCallback cb, HttpContext context, string clientID)
        {
            Context = context;
            ClientID = clientID;
            _callback = cb;
        }

        //当异步完成时调用该方法
        public void SetComplete()
        {
            IsCompleted = true;
            if (_callback != null)
            {
                _callback(this);
            }
        }

        //存储客户端标识
        public string ClientID
        {
            get;
            private set;
        }

        public HttpContext Context
        {
            get;
            private set;
        }

        public object AsyncState
        {
            get { return null; }
        }

        //由于ASP.NET不会等待WEB异步方法，所以不使用此对象
        public WaitHandle AsyncWaitHandle
        {
            get { throw new NotImplementedException(); }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get;
            private set;
        }
    }
}
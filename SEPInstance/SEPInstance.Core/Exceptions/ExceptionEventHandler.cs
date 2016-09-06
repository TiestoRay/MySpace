using Abp.Dependency;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;

namespace SEPInstance.Exceptions
{
    /// <summary>
    /// 共通异常事件的处理
    /// </summary>
    public class ExceptionEventHandler : IEventHandler<AbpHandledExceptionData>, ITransientDependency
    {
        public ILogger Logger { get; set; }

        public ExceptionEventHandler()
        {
            Logger = NullLogger.Instance;
        }
        /// <summary>
        /// 处理异常 eventData.Exception
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(AbpHandledExceptionData eventData)
        {
            //异常的处理 :eventData.Exception
            //Logger.Error("自定义异常记录：", eventData.Exception);
        }
    }
}

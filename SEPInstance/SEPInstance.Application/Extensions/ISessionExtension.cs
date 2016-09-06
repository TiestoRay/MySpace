using Abp.Runtime.Session;
using SEPInstance.Dto.OutputDto.Sessions;

namespace SEPInstance.Extensions
{
    /// <summary>
    /// Session扩展类
    /// </summary>
    public interface ISessionExtension : IAbpSession
    {
        /// <summary>
        /// SessionStore类
        /// </summary>
        SessionStore SessionStore { get; }
    }
}

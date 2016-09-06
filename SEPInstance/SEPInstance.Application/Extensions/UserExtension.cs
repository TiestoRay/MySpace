using Abp.Dependency;
using SEPInstance.Dto.InputDto.Sessions;

namespace SEPInstance.Extensions
{
    /// <summary>
    /// 用户信息扩展接口的实现
    /// </summary>
    public class UserExtension:IUserExtension,ITransientDependency
    {
        /// <summary>
        /// 注入session
        /// </summary>
        private readonly ISessionExtension _session;
        public UserExtension(ISessionExtension session) {
            _session = session;
        }

        /// <summary>
        /// 获取当前用户登录信息
        /// </summary>
        /// <returns>用户信息类（可自定义扩展）</returns>
        public UserLoginInfoDto GetCurrentUser() {
            var user = _session.SessionStore.LoginInfo.User;
            return user;
        }

        /// <summary>
        /// 获取当前用户Id
        /// 用户未登录会报错,请先判断是否已登陆<see cref="IsLogin"/>)
        /// </summary>
        /// <returns>当前登录用户Id</returns>
        public long GetCurrentUserId() {
            long userId = _session.UserId.Value;
            return userId;
        }

        /// <summary>
        /// 判断当前用户是否已经登陆
        /// </summary>
        /// <returns>true已登录，false未登录</returns>
        public bool IsLogin() {
            return _session.UserId.HasValue;
        }
    }
}

using SEPInstance.Dto.InputDto.Sessions;

namespace SEPInstance.Extensions
{
    /// <summary>
    /// 用户信息获取的扩展
    /// </summary>
    public interface IUserExtension
    {
        /// <summary>
        /// 获取当前用户登录信息
        /// </summary>
        /// <returns>用户信息类（可自定义扩展）</returns>
        UserLoginInfoDto GetCurrentUser();

        /// <summary>
        /// 获取当前用户Id
        /// 用户未登录会报错,请先判断是否已登陆<see cref="IsLogin"/>)
        /// </summary>
        /// <returns>当前登录用户Id</returns>
        long GetCurrentUserId();

        /// <summary>
        /// 判断当前用户是否已经登陆
        /// </summary>
        /// <returns>true已登录，false未登录</returns>
        bool IsLogin();
    }
}

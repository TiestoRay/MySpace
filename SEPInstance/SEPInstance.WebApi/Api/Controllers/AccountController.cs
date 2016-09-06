using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Security.Claims;
using System.Web;
using Abp.Authorization.Users;
using Abp.UI;
using Abp.Web.Models;
using Abp.WebApi.Controllers;
using Abp.AutoMapper;
using SEPInstance.Api.Models;
using SEPInstance.Authorization.Roles;
using SEPInstance.MultiTenancy;
using SEPInstance.Users;
using SEPInstance.IAppService;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity;


namespace SEPInstance.Api.Controllers
{
    public class AccountController : AbpApiController
    {
        /// <summary>
        /// 不记名令牌验证
        /// </summary>
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        /// <summary>
        /// 用户管理者
        /// </summary>
        private readonly UserManager _userManager;

        /// <summary>
        /// Session应用服务注入
        /// </summary>
        private readonly ISessionAppService _sessionAppService;

        ///// <summary>
        ///// 获取权限认证管理接口
        ///// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        static AccountController()
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userManager">用户管理者</param>
        public AccountController(UserManager userManager, ISessionAppService sessionAppService)
        {
            _userManager = userManager;
            _sessionAppService = sessionAppService;
        }

        /// <summary>
        /// 进行登陆并获取token认证
        /// </summary>
        /// <param name="loginModel">登陆信息</param>
        /// <returns>封装的token</returns>
        [HttpPost]
        public async Task<AjaxResponse> Authenticate(LoginModel loginModel)
        {
            CheckModelState();

            var loginResult = await GetLoginResultAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                loginModel.TenancyName
                );
            await SignInAsync(loginResult.User, loginResult.Identity, false);
            var ticket = new AuthenticationTicket(loginResult.Identity, new AuthenticationProperties());

            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));

            return new AjaxResponse(OAuthBearerOptions.AccessTokenFormat.Protect(ticket));
        }

        private async Task<AbpUserManager<Tenant, Role, User>.AbpLoginResult> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _userManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }
        /// <summary>
        /// 用户登录系统
        /// </summary>
        /// <param name="user"></param>
        /// <param name="identity"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            var info = user.MapTo<SEPInstance.Dto.InputDto.Sessions.UserLoginInfoDto>();
            _sessionAppService.SetCurrentLoginInformations(new SEPInstance.Dto.OutputDto.Sessions.GetCurrentLoginInformationsOutput { User = info }, identity);
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        }

        private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new ApplicationException("成功不会调用此方法!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("登陆失败"), L("无效的用户名或密码"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("登陆失败"), L("无此租户：{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("登陆失败"), L("该租户已禁用", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("登陆失败"), L("该用户已禁用", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("登陆失败"), "你的电子邮件地址未确认。不能登陆。");
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("登陆失败原因: " + result);
                    return new UserFriendlyException(L("登陆失败"));
            }
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("无效的请求!");
            }
        }
    }
}

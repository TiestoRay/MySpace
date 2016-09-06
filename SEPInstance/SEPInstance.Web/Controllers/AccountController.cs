using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Models;
using SEPInstance.Authorization.Roles;
using SEPInstance.MultiTenancy;
using SEPInstance.Users;
using SEPInstance.Web.Models.Account;
using SEPInstance.IAppService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace SEPInstance.Web.Controllers
{
    /// <summary>
    /// 用户登陆/注销控制器类
    /// </summary>
    public class AccountController : SEPInstanceControllerBase
    {
        /// <summary>
        /// 用户管理者注入
        /// </summary>
        private readonly UserManager _userManager;

        /// <summary>
        /// 多租户配置注入
        /// </summary>
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        /// <summary>
        /// Session应用服务注入
        /// </summary>
        private readonly ISessionAppService _sessionAppService;

        /// <summary>
        /// 获取权限认证管理接口
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userManager">用户管理者</param>
        /// <param name="multiTenancyConfig">多租户配置</param>
        /// <param name="sessionAppService">Session应用服务</param>
        public AccountController(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            ISessionAppService sessionAppService)
        {
            _userManager = userManager;
            _multiTenancyConfig = multiTenancyConfig;
            _sessionAppService = sessionAppService;
        }


        #region 登陆 / 注销

        /// <summary>
        /// 登陆页面
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult Login(string returnUrl = "")
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            return View(
                new LoginFormViewModel
                {
                    ReturnUrl = returnUrl,
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
                });
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckCode()
        {
            string code = _userManager.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = _userManager.CreateValidateCodePic(code);
            return File(bytes, @"image/png");
        }
        /// <summary>
        /// 验证码验证
        /// </summary>
        /// <param name="code"></param>
        [HttpPost]
        public ContentResult CheckCode(string code)
        {
            bool flag = Session["ValidateCode"].ToString().ToUpper().Equals(code.ToUpper());
            if (flag)
            {
                return Content("true");
            }
            else
            {
                return Content("false");
            }
        }

        /// <summary>
        /// 登陆的提交
        /// </summary>
        /// <param name="loginModel"></param>
        /// <param name="returnUrl"></param>
        /// <param name="returnUrlHash"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAuditing]
        public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            CheckModelState();
            loginModel.TenancyName = Tenant.DefaultTenantName;
            var loginResult = await GetLoginResultAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                loginModel.TenancyName
                );

            await SignInAsync(loginResult.User, loginResult.Identity, loginModel.RememberMe);

            returnUrl = "/Home";

            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        /// <summary>
        /// 获取登录结果
        /// </summary>
        /// <param name="usernameOrEmailAddress"></param>
        /// <param name="password"></param>
        /// <param name="tenancyName"></param>
        /// <returns></returns>
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
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        }

        /// <summary>
        /// 异常信息及登陆失败提示
        /// </summary>
        /// <param name="result"></param>
        /// <param name="usernameOrEmailAddress"></param>
        /// <param name="tenancyName"></param>
        /// <returns></returns>
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
                default:
                    Logger.Warn("登陆失败原因: " + result);
                    return new UserFriendlyException(L("登陆失败"));
            }
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        #endregion
    }
}
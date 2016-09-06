using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Newtonsoft.Json;
using SEPInstance.IDao.IEntityRepository;
using SEPInstance.BreadCrumb;
using SEPInstance.Dto.OutputDto.Sessions;
using SEPInstance.Dto.InputDto.Sessions;
using SEPInstance.IAppService;
using SEPInstance.Extensions;
using SEPInstance.Users;


namespace SEPInstance.AppService
{
    /// <summary>
    /// Session应用服务实现类
    /// 存储当前登录用户信息和面包屑路径。
    /// </summary>
    [DisableAuditing]
    public class SessionAppService : ISessionAppService,ITransientDependency
    {
        /// <summary>
        /// Session扩展接口
        /// </summary>
        private readonly ISessionExtension _session;

        /// <summary>
        /// 用户仓储接口
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Session应用服务构造函数
        /// </summary>
        /// <param name="session">ISessionExtension</param>
        /// <param name="userRepository">IUserRepository</param>
        public SessionAppService(ISessionExtension session, IUserRepository userRepository)
        {
            _session = session;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>Task<GetCurrentLoginInformationsOutput></returns>
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            GetCurrentLoginInformationsOutput info = null;
            if (_session.UserId.HasValue)
            {
                var data = _session.SessionStore;
                if (data != null)
                {
                    info = data.LoginInfo;
                    return info;
                }
                else
                {
                    var user = _userRepository.Get(_session.UserId.Value);
                    info = new GetCurrentLoginInformationsOutput
                    {
                        User = user.MapTo<UserLoginInfoDto>()
                    };
                    SetCurrentLoginInformations(info,Thread.CurrentPrincipal.Identity as ClaimsIdentity);
                }
            }

            return info;
        }

       /// <summary>
        /// 设置登录信息
       /// </summary>
       /// <param name="input">当前登录信息</param>
       /// <param name="identity">声明</param>
        public void SetCurrentLoginInformations(GetCurrentLoginInformationsOutput input,ClaimsIdentity identity)
        {
            if (_session.UserId.HasValue || (input.User!=null && input.User.Id>0))
            {
                var userId=_session.UserId.HasValue?_session.UserId.Value:input.User.Id;

                if (identity.HasClaim(m => m.Type == SEPInstanceConsts.SessionExtensionClaimType))
                {
                    var claim = identity.Claims.FirstOrDefault(m => m.Type == SEPInstanceConsts.SessionExtensionClaimType);
                    var old = JsonConvert.DeserializeObject<SessionStore>(claim.Value);
                    identity.RemoveClaim(claim);
                    old.LoginInfo = input;
                    string data = JsonConvert.SerializeObject(old);
                    identity.AddClaim(new Claim(SEPInstanceConsts.SessionExtensionClaimType, data));
                }
                else
                {
                    SessionStore store = new SessionStore { LoginInfo = input };
                    string data = JsonConvert.SerializeObject(store);
                    identity.AddClaim(new Claim(SEPInstanceConsts.SessionExtensionClaimType, data));
                }
            }
        }

        /// <summary>
        /// 面包屑设置
        /// </summary>
        /// <param name="model">面包屑数据</param>
        /// <param name="identity">基于声明的验证</param>
        public void PushToUserCrumb(BreadCrumbModel model, ClaimsIdentity identity)
        {
            if (_session.UserId.HasValue)
            {
                if (identity.HasClaim(m => m.Type == SEPInstanceConsts.SessionExtensionClaimType))
                {
                    var claim = identity.Claims.FirstOrDefault(m => m.Type == SEPInstanceConsts.SessionExtensionClaimType);
                    var info = JsonConvert.DeserializeObject<SessionStore>(claim.Value);
                    identity.RemoveClaim(claim);
                    bool delete = false;
                    if (info.BreadCrumbInfo == null)
                        info.BreadCrumbInfo = new List<BreadCrumbModel>();
                    for (int i = 0; i < info.BreadCrumbInfo.Count(); i++)
                    {
                        if (info.BreadCrumbInfo[i].Name == model.Name || delete)
                        {
                            info.BreadCrumbInfo.RemoveAt(i);
                            delete = true;
                            i--;
                        }
                    }
                    info.BreadCrumbInfo.Add(model);
                    info.LastUpdateTime = DateTime.Now;
                    string data = JsonConvert.SerializeObject(info);
                    identity.AddClaim(new Claim(SEPInstanceConsts.SessionExtensionClaimType, data));
                }
                else
                {
                    SessionStore store = new SessionStore { BreadCrumbInfo = new List<BreadCrumbModel>() { model }, LastUpdateTime = DateTime.Now };
                    string data = JsonConvert.SerializeObject(store);
                    identity.AddClaim(new Claim(SEPInstanceConsts.SessionExtensionClaimType, data));
                }
            }
        }

        /// <summary>
        /// 获取当前面包屑
        /// </summary>
        /// <returns>List<BreadCrumbModel></returns>
        public List<BreadCrumbModel> GetUserCrumb()
        {
            if (_session.UserId.HasValue)
            {
                var info = _session.SessionStore;
                if (info != null)
                {
                    return info.BreadCrumbInfo;
                }

            }
            return new List<BreadCrumbModel>();
        }
    }
}

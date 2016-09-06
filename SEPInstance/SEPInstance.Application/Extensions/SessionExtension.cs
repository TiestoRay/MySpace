using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using Abp.Configuration.Startup;
using Abp.Runtime.Session;
using Abp.Runtime.Security;
using Abp.Dependency;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SEPInstance.Dto.OutputDto.Sessions;

namespace SEPInstance.Extensions
{
    /// <summary>
    /// 扩展Session类
    /// </summary>
    public class SessionExtension : ClaimsAbpSession, ISessionExtension,ISingletonDependency
    {
        /// <summary>
        /// 扩展Session构造函数
        /// </summary>
        /// <param name="multiTenancy">IMultiTenancyConfig</param>
        public SessionExtension(IMultiTenancyConfig multiTenancy) : base(multiTenancy)
        {
        }

        /// <summary>
        /// SessionStore类实现
        /// </summary>
        public virtual SessionStore SessionStore
        {
            get {
                var claimPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
                if (claimPrincipal == null)
                    return null;
                var identity = claimPrincipal.Identity as ClaimsIdentity;
                if (identity == null)
                    return null;
                var claim = identity.Claims.FirstOrDefault(m => m.Type == SEPInstanceConsts.SessionExtensionClaimType);
                if (claim == null || string.IsNullOrEmpty(claim.Value))
                    return null;
                var result = JsonConvert.DeserializeObject<SessionStore>(claim.Value);
                return result;
            }
        }
    }
}

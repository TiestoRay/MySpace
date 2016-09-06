using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;

namespace SEPInstance.Web.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract class SEPInstanceControllerBase : AbpController
    {
        /// <summary>
        /// 检查模型后台验证是否通过
        /// </summary>
        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("输入信息无效"));
            }
        }

        /// <summary>
        /// 检查用户身份验证
        /// </summary>
        /// <param name="identityResult"></param>
        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
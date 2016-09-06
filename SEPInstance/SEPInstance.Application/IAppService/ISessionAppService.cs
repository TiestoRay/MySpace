using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SEPInstance.Dto.OutputDto.Sessions;
using SEPInstance.BreadCrumb;
using System.Security.Claims;

namespace SEPInstance.IAppService
{
    /// <summary>
    /// Session应用服务
    /// </summary>
    public interface ISessionAppService
    {
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns>当前用户信息</returns>
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="input">用户信息</param>
        /// <param name="identity">用户身份声明验证</param>
        void SetCurrentLoginInformations(GetCurrentLoginInformationsOutput input,ClaimsIdentity identity);

        /// <summary>
        /// 面包屑设置
        /// </summary>
        /// <param name="model">面包屑数据</param>
        /// <param name="identity">用户身份声明验证</param>
        void PushToUserCrumb(BreadCrumbModel model, ClaimsIdentity identity);

        /// <summary>
        /// 获取当前用户的面包屑
        /// </summary>
        /// <returns>面包屑列表</returns>
        List<BreadCrumbModel> GetUserCrumb();
    }
}

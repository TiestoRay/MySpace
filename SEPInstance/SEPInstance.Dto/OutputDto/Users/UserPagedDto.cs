using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEPInstance.Dto.OutputDto.Pager;
using SEPInstance.Users;
using Webdiyer.WebControls.Mvc;

namespace SEPInstance.Dto.OutputDto.Users
{
    /// <summary>
    /// 用户分页dto
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UserPagedDto<T> : PagedListDto<User, T>
        where T : class
    {
        /// <summary>
        /// 用户分页构造方法
        /// </summary>
        /// <param name="SourceList">转换前的用户分页列表</param>
        public UserPagedDto(PagedList<User> SourceList)
            : base(SourceList)
        {
        }
    }
}

using SEPInstance.Dto.OutputDto.Sessions;
using SEPInstance.Dto.InputDto.Sessions;

namespace SEPInstance.Web.Models.Layout
{
    /// <summary>
    /// ????????????????????????
    /// </summary>
    public class UserMenuOrLoginLinkViewModel
    {
        /// <summary>
        /// ????????????????
        /// </summary>
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        /// <summary>
        /// ??????????????
        /// </summary>
        public bool IsMultiTenancyEnabled { get; set; }

        /// <summary>
        /// ??????????????
        /// </summary>
        /// <returns></returns>
        public string GetShownLoginName()
        {
            var userName = "<span id=\"HeaderCurrentUserName\">" + LoginInformations.User.UserName + "</span>";
            return userName;
        }
    }
}
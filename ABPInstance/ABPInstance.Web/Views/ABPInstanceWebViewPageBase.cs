using Abp.Web.Mvc.Views;

namespace ABPInstance.Web.Views
{
    public abstract class ABPInstanceWebViewPageBase : ABPInstanceWebViewPageBase<dynamic>
    {

    }

    public abstract class ABPInstanceWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected ABPInstanceWebViewPageBase()
        {
            LocalizationSourceName = ABPInstanceConsts.LocalizationSourceName;
        }
    }
}
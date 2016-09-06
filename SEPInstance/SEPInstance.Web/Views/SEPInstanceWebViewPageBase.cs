using Abp.Web.Mvc.Views;

namespace SEPInstance.Web.Views
{
    public abstract class SEPInstanceWebViewPageBase : SEPInstanceWebViewPageBase<dynamic>
    {

    }

    public abstract class SEPInstanceWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected SEPInstanceWebViewPageBase()
        {
            LocalizationSourceName = SEPInstanceConsts.LocalizationSourceName;
        }
    }
}
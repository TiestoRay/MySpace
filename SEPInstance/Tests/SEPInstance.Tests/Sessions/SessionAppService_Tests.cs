using System.Threading.Tasks;
using SEPInstance.IAppService;
using Shouldly;
using Xunit;

namespace SEPInstance.Tests.Sessions
{
    public class SessionAppService_Tests : SEPInstanceTestBase
    {
        private readonly ISessionAppService _sessionAppService;

        public SessionAppService_Tests()
        {
            _sessionAppService = Resolve<ISessionAppService>();
        }

    }
}

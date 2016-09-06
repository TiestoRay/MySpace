using System.Data.Entity;
using System.Threading.Tasks;
using SEPInstance.Users;
using SEPInstance.IAppService;
using Shouldly;
using Xunit;
using SEPInstance.Dto.InputDto.Users;

namespace SEPInstance.Tests.Users
{
    public class UserAppService_Tests : SEPInstanceTestBase
    {
        private readonly IUserAppService _userAppService;

        public UserAppService_Tests()
        {
            _userAppService = Resolve<IUserAppService>();
        }

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        [Fact]
        public void GetUsers_Test()
        {
            var output = _userAppService.GetUsersPagedList(new UserListInput { PageIndex = 1 });

            output.Success.ShouldBe(true);
        }
    }
}

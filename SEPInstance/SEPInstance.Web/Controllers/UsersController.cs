using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using SEPInstance.Authorization;
using SEPInstance.Users;
using SEPInstance.Dto.InputDto.Users;
using SEPInstance.Dto.InputDto.Search;
using SEPInstance.MultiTenancy;
using SEPInstance.Authorization.Roles;
using Abp.Domain.Uow;
using Abp.Web.Mvc.Models;
using Abp.UI;
using Abp.Extensions;
using Microsoft.AspNet.Identity;
using SEPInstance.IAppService;
using SEPInstance.Web.Filters;
using Abp.Web.Models;

namespace SEPInstance.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Users)]
    public class UsersController : SEPInstanceControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public UsersController(IUserAppService userAppService, IUnitOfWorkManager unitOfWorkManager, UserManager userManager)
        {
            _userAppService = userAppService;
            _unitOfWorkManager = unitOfWorkManager;
            _userManager = userManager;
        }

        /// <summary>
        /// 用户列表的查询页面
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [BreadCrumb("用户列表")]
        public ActionResult UsersList(UserListInput search)
        {
            var output = _userAppService.GetUsersPagedList(search);
            if (output.Success)
                return View(output.DestinationList);
            else
                throw new UserFriendlyException("服务器错误", "查询结果出现错误");
        }

        /// <summary>
        /// 进入新增用户页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BreadCrumb("新增用户")]
        public ActionResult AddUser()
        {
            var model = new CreateUserInput();
            return View(model);
        }

        /// <summary>
        /// 新增用户表单的提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [UnitOfWork]
        public virtual async Task<JsonResult> AddUser(CreateUserInput model)
        {
            try
            {
                CheckModelState();

                var user = new User
                {
                    TenantId = AbpSession.TenantId.Value,
                    Name = model.Name,
                    Surname = model.Name,
                    EmailAddress = model.EmailAddress,
                    IsActive = true,
                    UserName = model.UserName,
                    Password = new PasswordHasher().HashPassword(model.Password)
                };

                CheckErrors(await _userManager.CreateAsync(user));
                await _unitOfWorkManager.Current.SaveChangesAsync();

                return Json(new AjaxResponse { Success = true });
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse { Success = false, Result = ex.Message });
            }
        }

        /// <summary>
        /// 进入编辑用户页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [BreadCrumb("编辑用户")]
        public ActionResult EditUser(long id)
        {
            var user = _userAppService.GetEditUserInfo(id);
            return View(user);
        }

        /// <summary>
        /// 编辑用户表单提交
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [UnitOfWork]
        public virtual JsonResult EditUser(EditUserInput input)
        {
            _userAppService.UpdateUser(input);
            return Json(new AjaxResponse { Success = true });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPost]
        [UnitOfWork]
        public virtual JsonResult DeleteUser(long id)
        {
            _userAppService.DeleteUser(id);
            return Json(new AjaxResponse { Success = true });
        }

        /// <summary>
        /// 进入用户分配角色的页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [BreadCrumb("用户分配角色")]
        public ActionResult UserRole(long id)
        {
            var roles = _userAppService.GetUserRoleSetList(id);

            return View(roles);
        }

        /// <summary>
        /// 用户分配角色的提交
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UserRole(UserRoleInput input)
        {
            await _userAppService.ResetUserRole(input);
            return Json(new AjaxResponse { Success = true });
        }

        /// <summary>
        /// 页面remote验证用户名是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult RemoteCheckExist(UserValidate input)
        {
            if (_userAppService.ValidateUserName(input))
                return Content("true");
            else
                return Content("false");
        }

        /// <summary>
        /// 页面remote验证邮箱是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult RemoteCheckEmailExist(UserValidate input)
        {
            if (_userAppService.ValidateUserEmail(input))
                return Content("true");
            else
                return Content("false");
        }
    }
}
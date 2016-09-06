using Abp.Authorization;
using Abp.Web.Mvc.Models;
using SEPInstance.Authorization;
using SEPInstance.IAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SEPInstance.Dto.InputDto.Roles;
using SEPInstance.Web.Filters;
using Abp.Web.Models;


namespace SEPInstance.Web.Controllers
{
    /// <summary>
    /// 角色管理控制器
    /// 继承自SEPInstanceControllerBase
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RolesController : SEPInstanceControllerBase
    {
        /// <summary>
        /// 注入角色应用服务接口
        /// </summary>
        private readonly IRoleAppService _roleAppService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleAppService"></param>
        public RolesController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [BreadCrumb("角色列表")]
        public ActionResult RolesList(RoleListInput input)
        {
            var pagedList = _roleAppService.GetPagedRoleList(input);
            return View(pagedList);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <returns></returns>
        [BreadCrumb("新增角色")]
        public ActionResult AddRoles()
        {
            return View();
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddRoles(CreateRoleInput input)
        {
            _roleAppService.CreateRole(input);
            return Json(new AjaxResponse { Success = true });
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BreadCrumb("编辑角色")]
        public ActionResult UpdateRole(int id)
        {
            var model = _roleAppService.EditRoleById(id);
            return View(model);
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateRole(EditRoleInput input)
        {
            _roleAppService.EditRole(input);
            return Json(new AjaxResponse { Success = true });
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteRoles(int id)
        {
            if (_roleAppService.GetUsersByRoleId(id) > 0)
            {
                return Json("0");
            }
            else
            {
                _roleAppService.DeleteRole(id);
                return Json(new AjaxResponse { Success = true });
            }
        }

        /// <summary>
        /// 权限分配
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [BreadCrumb("角色权限分配")]
        public async Task<ActionResult> RoleAuthorization(int id)
        {
            var currentPermissions = await _roleAppService.GetPermissionsByRole(id);
            var allPermissions = PermissionNames.GetAuthorizationTree();
            ViewBag.roleId = id;
            ViewBag.permissions = currentPermissions;
            return View(allPermissions);
        }

        /// <summary>
        /// 权限分配
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> RoleAuthorization(UpdateRolePermissionsInput input)
        {
            await _roleAppService.UpdateRolePermissions(input);
            return Json(new AjaxResponse { Success = true });
        }
    }
}
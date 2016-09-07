﻿using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace ABPInstance.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ABPInstanceControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}
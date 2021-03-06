﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using OneSignalAPPMngr.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OneSignalAPPMngr.Controllers
{
	[Authorize]
	public class UsersController : BaseController
    {
	
		public ActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				var user = User.Identity;
				ViewBag.Name = user.Name;
	
				ViewBag.displayMenu = "No";

				if (isAdminUser())
				{
					ViewBag.displayMenu = "Yes";
				}
				return View();
			}
			else
			{
				ViewBag.Name = "Not Logged IN";
			}


			return View();


		}
	}
}
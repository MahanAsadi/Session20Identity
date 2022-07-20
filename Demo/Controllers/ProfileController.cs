using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize(Roles ="user")]
        public IActionResult Index()
        {
            string name = User.Identity.Name;
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [Authorize(Roles ="superAdmin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
          List<string> roles=  roleManager.Roles.Select(a=>a.Name).ToList();
            return View(roles);
        }

        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = roleName
                };
                await roleManager.CreateAsync(role);
            }
            return RedirectToAction("index");
        }
    }
}

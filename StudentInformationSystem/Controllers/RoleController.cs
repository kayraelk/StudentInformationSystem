using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentInformationSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInformationSystem.Controllers
{
    [Authorize(Policy = "ManageRoles")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ManageUserRoles()
        {
            var users = _userManager.Users;
            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                userRolesViewModel.Add(new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = userRoles
                });
            }

            return View(userRolesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.AddToRolesAsync(user, roles.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    // Handle errors
                }

                result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(roles).ToArray<string>());

                if (!result.Succeeded)
                {
                    // Handle errors
                }
            }

            return RedirectToAction("ManageUserRoles");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheBugTracker.Extensions;
using TheBugTracker.Models;
using TheBugTracker.Models.ViewModels;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Controllers
{

    [Authorize] // will be bounced to back to login page if not logged in
    public class UserRolesController : Controller
    {
        private readonly IBTRolesService _rolesService;
        private readonly IBTCompanyInfoService _companyInfoService;

        public UserRolesController(IBTRolesService rolesService, IBTCompanyInfoService companyInfoService)
        {
            _rolesService = rolesService;
            _companyInfoService = companyInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles()
        {
            // how to make get action work

            // Add an instance of the ViewModel as a List (model)
            List<ManageUserRolesViewModel> model = new();

            // Get CompanyId
            int companyId = User.Identity.GetCompanyId().Value;  

            // Gt all company users
            List<BTUser> users = await _companyInfoService.GetAllMembersAsync(companyId);

            // Loop over the users to populate the ViewModel
            // - instantiate ViewModel
            // - use _rolesService
            // - Create multi-selects
            foreach (BTUser user in users)
            {
                ManageUserRolesViewModel viewModel = new();
                viewModel.BTUser = user;
                IEnumerable<string> selected = await _rolesService.GetUserRolesAsync(user);
                viewModel.Roles = new MultiSelectList(await _rolesService.GetRolesAsync(), "Name", "Name", selected); // 2nd and 3rd param, what I need, what they see, selected is current role

                model.Add(viewModel);

            }


            // Return the model to the View
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // prevents cross-platform submissions
        public async Task<IActionResult> ManageUserRoles(ManageUserRolesViewModel member)
        {
            // Get the company ID
            int companyId = User.Identity.GetCompanyId().Value; // get company ID from logged in user, since they will be same company

            // Instantiate the BTUser
            BTUser btUser = (await _companyInfoService.GetAllMembersAsync(companyId)).FirstOrDefault(u => u.Id == member.BTUser.Id);


            // Get Roles for the User
            IEnumerable<string> roles = await _rolesService.GetUserRolesAsync(btUser);

            // Grab the selected role
            string userRole = member.SelectedRoles.FirstOrDefault();

            if (!string.IsNullOrEmpty(userRole))
            {
                // Remove User from their roles
                if(await _rolesService.RemoveUserFromRolesAsync(btUser, roles)) // if removing roles was succesful, add them to new role
                {
                    // Add user to the new role
                    await _rolesService.AddUserToRoleAsync(btUser, userRole);
                }

            }

            // Navigate back to the View
            return RedirectToAction(nameof(ManageUserRoles)); // navigate right back to this page, the get method, to show results

        }
    }
}

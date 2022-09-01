using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheBugTracker.Models.ViewModels
{
    public class ManageUserRolesViewModel
    {
        // view model, unlike data model which built for managing and updating data, will be a means to transport data around in the form as we need it
        public BTUser BTUser { get; set; }
        public MultiSelectList Roles { get; set; } // allows admin to see role user is currently in, and the roles they can be assigned to
        public List<String> SelectedRoles { get; set; }



    }
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ISystem.WebUI.Models.ViewModels
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        //public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}

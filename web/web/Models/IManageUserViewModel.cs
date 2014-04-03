using System;
namespace Web.Models
{
    interface IManageUserViewModel
    {
        string ConfirmPassword { get; set; }
        string NewPassword { get; set; }
        string OldPassword { get; set; }
    }
}

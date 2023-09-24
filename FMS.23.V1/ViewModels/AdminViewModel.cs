using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FMS._23.V1.ViewModels
{
    public class AdminViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string PhoneNo { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [ForeignKey("Role")]
        public string RoleID { get; set; }
        public string DepotId { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        public List<SelectListItem> RoleList { get; set; }
        public List<SelectListItem> DepotList { get; set; }
        public List<SelectListItem> UserList { get; set; }
        public virtual IdentityRole Role { get; set; }
        public virtual IdentityUser User { get; set; }
        public string Token { get; set; }
    }
}

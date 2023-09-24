using System.ComponentModel.DataAnnotations;

namespace FMS._23.V1.ViewModels
{
    public class LoginResponse
    {
        [Key]
        public int Result { get; set; }
        // public string Msg { get; set; }
        //public int RoleId { get; set; }
        //public string RoleName { get; set; }
        // public string LoginId { get; set; }
        public string Name { get; set; }
        public string Db_Name { get; set; }


        // public string CCode { get; set; }
        // public int LevelId { get; set; }
    }

    public class LoginViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }

    //public class GResposnse
    //{
    //    [Key]
    //    public int Result { get; set; }
    //    public string Msg { get; set; }
    //    public string Code { get; set; }
    //}
}

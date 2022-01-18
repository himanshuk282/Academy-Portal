using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Academy_Portal.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"[a-zA-Z]+",ErrorMessage ="Please enter alphabets only in First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"[a-zA-Z]+",ErrorMessage ="Please enter alphabets only in Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Editable(true)]
        public DateTime? DoB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        [RegularExpression(@"^[0-9]{10}$",ErrorMessage ="Please enter 10 digits only in Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //Can be Admin,Faculty,Employee
        [Display(Name ="User Category")]
        public string UserCategory { get; set; }
        [Display(Name ="User ID")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "What city were you born in?")]
        [MaxLength(50)]
        public string SecurityParameter1 { get; set; }

        [Required]
        [Display(Name = "What is your oldest sibling’s last name?")]
        [MaxLength(50)]
        public string SecurityParameter2 { get; set; }

        [Required]
        [Display(Name = "In what city or town did your parents meet?")]
        [MaxLength(50)]
        public string SecurityParameter3 { get; set; }
        //Use only when we are in faculty
        public string Proficiencylevel { get; set; }
        public int TotalHoursOfTeaching { get; set; }
        public int SkillID { get; set; }
        public int ModuleID { get; set; }
        //Properties to Display Drop Down list in the registration view model
        public ICollection<Skill> Skills { get; set; }
        public ICollection<Module> Modules { get; set; }//Use multiple option in the dropdown list
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

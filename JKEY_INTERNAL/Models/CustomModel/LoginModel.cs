using System.ComponentModel.DataAnnotations;

namespace JKEY_INTERNAL.Models.CustomModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is not null")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is not null")]
        [StringLength(50), MinLength(6)]
        public string PassWord { get; set; }
    }
}

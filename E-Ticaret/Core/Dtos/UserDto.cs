using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class UserDto
    {
    
        [Required(ErrorMessage = "İsim Alanı Boş Bırakılamaz")]
        [Display(Name = "İsim :")]
        public string UserNameMain { get; set; }

        [EmailAddress(ErrorMessage = "Email Format Yanlış")]
        [Required(ErrorMessage = "Email Alanı Boş Bırakılamaz")]
        [Display(Name = "Email :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Alanı Boş Bırakılamaz")]
        [Display(Name = "Telefon :")]
        public string PhoneNumber { get; set; }
    }
}

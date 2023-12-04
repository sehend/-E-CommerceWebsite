using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class SignUpDto
    {
       

        [Required(ErrorMessage = "Kulanıcı Ad Alanı Boş Bırakılamaz")]
        [Display(Name = "Kulanıcı Adı :")]
        public string UserNameMain { get; set; }

        [EmailAddress(ErrorMessage = "Email Format Yanlış")]
        [Required(ErrorMessage = "Email Alanı Boş Bırakılamaz")]
        [Display(Name = "Email :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Alanı Boş Bırakılamaz")]
        [Display(Name = "Telefon :")]
        public string Phone { get; set; }

        
        [Display(Name = "Şifre :")]
        [MinLength(6, ErrorMessage = "Şifreniz En az 6 Karakter Olabilir")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifre Aynı Degildir")]
        [Required(ErrorMessage = "Şifre Tekrar Alanı Boş Bırakılamaz")]
        [Display(Name = "Şifre Tekrar :")]
        [MinLength(6, ErrorMessage = "Şifreniz En az 6 Karakter Olabilir")]
        public string PasswordConfirm { get; set; }
    }
}

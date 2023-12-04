using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class NavbarUnderCategory
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Katagory Seçim Alanı Boş Bırakılamaz")]
        [Display(Name = "Katagory Ad :")]
        public string CategoryName { get; set; }

       
        [Required(ErrorMessage = "Alt Katagory Adı Alanı Boş Bırakılamaz")]
        [Display(Name = "Alt Katagory Ad :")]
        public string UnderCategoryName { get; set; }
    }
}

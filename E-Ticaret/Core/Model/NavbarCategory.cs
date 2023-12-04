using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class NavbarCategory
    {
        public string Id { get; set; }

       
        [Required(ErrorMessage = "Katagory Adı Alanı Boş Bırakılamaz")]
        [Display(Name = "Katagory Ad :")]
        public string CategoryName { get; set; }

    }
}

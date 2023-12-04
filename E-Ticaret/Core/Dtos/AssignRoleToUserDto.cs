using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class AssignRoleToUserDto
    {
        public string Id { get; set; }

       
        [Required(ErrorMessage = "İsim Alanı Boş Bırakılamaz")]
        [Display(Name = "İsim :")]
        public string Name { get; set; }

        public bool Exist { get; set; }
    }
}

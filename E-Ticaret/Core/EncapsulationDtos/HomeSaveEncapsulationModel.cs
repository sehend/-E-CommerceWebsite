using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EncapsulationDtos
{
    public class HomeSaveEncapsulationModel
    {
        public List<NavbarCategory> navbarCategories { get; set; }

        public NavbarUnderCategory navbarUnderCategories { get; set; }
    }
}

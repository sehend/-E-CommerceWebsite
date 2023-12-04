using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EncapsulationDtos
{
    public class HomeIndexEncapsulationModel
    {
        public List<NavbarCategory> navbarCategories { get; set; }

        public List<NavbarUnderCategory> navbarUnderCategories { get; set; }
    }
}

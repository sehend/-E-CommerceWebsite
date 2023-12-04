using Core.Dtos;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EncapsulationDtos
{
    public class ProductAndUnderNavbarEncapsulationModel
    {
        public List<NavbarUnderCategory> navbarUnderCategories { get; set; }

        public ProductDto ProductDto { get; set; }

        public Product Product { get; set; }
    }
}

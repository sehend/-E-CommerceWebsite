using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class ProductDto
    {
        public string? Id { get; set; }


        [Required(ErrorMessage = "Alt Katagory Adı Alanı Boş Bırakılamaz")]
        [Display(Name = "Alt Katagory Ad :")]
        public string? NavbarProductUnderCategoriesName { get; set; }

        [Required(ErrorMessage = "İlk Resim Alanı Boş Bırakılamaz")]
        [Display(Name = "İlk Resim :")]
        public IFormFile image { get; set; }

        [Display(Name = "İkinci Resim :")]
        public IFormFile image2 { get; set; }

        [Display(Name = "Üçüncü Resim :")]
        public IFormFile image3 { get; set; }


        [Required(ErrorMessage = "Ürün Adı Alanı Boş Bırakılamaz")]
        [Display(Name = "Ürün Ad :")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Stock Miktarı Alanı Boş Bırakılamaz")]
        [Display(Name = "Stock Miktarı :")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Açıklama Alanı Boş Bırakılamaz")]
        [Display(Name = "Açıklama Miktarı :")]
        public string? Explanation { get; set; }

        public decimal OldPrices { get; set; }


        [Required(ErrorMessage = "Fiyat Alanı Boş Bırakılamaz")]
        [Display(Name = "Fiyat :")]
        public decimal Prices { get; set; }
    }
}

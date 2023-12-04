using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class MainPage
    {
        public string? Id { get; set; }

     
        [Required(ErrorMessage = "Alt Katagory Adı Alanı Boş Bırakılamaz")]
        [Display(Name = "Alt Katagory Ad :")]
        public string? NavbarProductUnderCategoriesName { get; set; }

        [Required(ErrorMessage = "İlk Resim Alanı Boş Bırakılamaz")]
        [Display(Name = "İlk Resim :")]
        public string? image { get; set; }

        [Display(Name = "İkinci Resim :")]
        public string? image2 { get; set; }

        [Display(Name = "Üçüncü Resim :")]
        public string? image3 { get; set; }

        [Required(ErrorMessage = "Ürün Adı Alanı Boş Bırakılamaz")]
        [Display(Name = "Ürün Ad :")]
        public string? ProductName { get; set; }

      
        [Required(ErrorMessage = "Stock Miktarı Alanı Boş Bırakılamaz")]
        [Display(Name = "Stock Miktarı :")]
        public int Stock { get; set; }


        [EmailAddress(ErrorMessage = "Bir Hata Oluştu....")]
        [Required(ErrorMessage = "Açıklama Alanı Boş Bırakılamaz")]
        [Display(Name = "Açıklama Miktarı :")]
        public string? Explanation { get; set; }

        public decimal OldPrices { get; set; }

       
        [Required(ErrorMessage = "Fiyat Alanı Boş Bırakılamaz")]
        [Display(Name = "Fiyat :")]
        public decimal Prices { get; set; }
    }
}

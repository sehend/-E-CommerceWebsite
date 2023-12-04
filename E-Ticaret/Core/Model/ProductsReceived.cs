using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class ProductsReceived
    {
        public string Id { get; set; }

        public string CustomerEmail { get; set; }

        public string image { get; set; }

        public string ProductName { get; set; }

        public decimal Prices { get; set; }

        public int Amount { get; set; }

        public decimal TotalPrices { get; set; }

        public DateTime Tarih { get; set; }
    }
}

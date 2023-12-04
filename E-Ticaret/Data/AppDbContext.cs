using Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<MainPage>  mainPages { get; set; }

        public DbSet<Product>   products { get; set; }

        public DbSet<NavbarCategory>    navbarCategories { get; set; }

        public DbSet<NavbarUnderCategory>    navbarUnderCategories { get; set; }

        public DbSet<Basket> baskets { get; set; }

        public DbSet<ProductsReceived>  productsReceiveds { get; set; }
    }
}

using Azure.Core;
using Core.Model;
using Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace E_Ticaret.Seeds
{
    public class PermissionSeed
    {
        public static async Task Seed(RoleManager<AppRole> roleManager, IServiceProvider serviceProvider)
        {

            var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            var hasAdminRole = await roleManager.RoleExistsAsync("AdminRole");
            var hasNavbarRole = await roleManager.RoleExistsAsync("NavbarRole");
            var hasMainPageRole = await roleManager.RoleExistsAsync("MainPageRole");
            var hasProductRole = await roleManager.RoleExistsAsync("ProductRole");
            var hasUserListRole = await roleManager.RoleExistsAsync("UserListRole");

            var hasMember = await roleManager.RoleExistsAsync("Member");

            if (!hasAdminRole)
            {
                await roleManager.CreateAsync(new AppRole() { Name = "AdminRole" });

                var basicRole = roleManager.FindByNameAsync("AdminRole");

            }

            if (!hasMember)
            {
                await roleManager.CreateAsync(new AppRole() { Name = "Member" });

                var MemberRole = roleManager.FindByNameAsync("Member");

            }

            if (!hasNavbarRole)
            {
                await roleManager.CreateAsync(new AppRole() { Name = "NavbarRole" });

                var MemberRole = roleManager.FindByNameAsync("NavbarRole");

            }

            if (!hasMainPageRole)
            {
                await roleManager.CreateAsync(new AppRole() { Name = "MainPageRole" });

                var MemberRole = roleManager.FindByNameAsync("MainPageRole");

            }

            if (!hasProductRole)
            {
                await roleManager.CreateAsync(new AppRole() { Name = "ProductRole" });

                var MemberRole = roleManager.FindByNameAsync("ProductRole");

            }

            if (!hasUserListRole)
            {
                await roleManager.CreateAsync(new AppRole() { Name = "UserListRole" });

                var MemberRole = roleManager.FindByNameAsync("UserListRole");

            }

            string chars = "abcçdefgğhiıjklmnoöprsştuüvyz";

            var random = new Random();

            var randomString = new string(Enumerable.Repeat(chars, 40).Select(s => s[random.Next(s.Length)]).ToArray());

            var model = await userManager.FindByEmailAsync("sehendsina@gmail.com");

            if (model == null)
            {


                var identityResult = await userManager.CreateAsync(new() { UserName = randomString, PhoneNumber = "01323213211112", Email = "sehendsina@gmail.com", UserNameMain = "Sehend0219" }, "Sehend0219");

                var request = await userManager.FindByEmailAsync("sehendsina@gmail.com");

                await userManager.AddToRoleAsync(request, "AdminRole");


            }
        }


    }
}

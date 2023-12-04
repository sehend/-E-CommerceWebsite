using Core.Model;
using Data;
using E_Ticaret.CustemValidatoins;
using E_Ticaret.Localizasion;
using Microsoft.AspNetCore.Identity;

namespace E_Ticaret.Extenion
{
    public static class StartupExtenions
    {

        public static void AddIDentityWithExtenions(this IServiceCollection services)
        {

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

            services.AddIdentity<AppUser, AppRole>(options =>
            {

                options.User.RequireUniqueEmail = true;

                options.User.AllowedUserNameCharacters = "abcçdefgğhiıjklmnoöprsştuüvyz1234567890_";

                options.Password.RequiredLength = 6;

                options.Password.RequireNonAlphanumeric = false;

                options.Password.RequireLowercase = true;

                options.Password.RequireUppercase = true;

                options.Password.RequireDigit = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

                options.Lockout.MaxFailedAccessAttempts = 3;


            }).AddPasswordValidator<PasswordValidator>()
            .AddUserValidator<UserValidatorMain>()
            .AddErrorDescriber<localizaionIdentityErrorDescriber>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();
        }


    }
}

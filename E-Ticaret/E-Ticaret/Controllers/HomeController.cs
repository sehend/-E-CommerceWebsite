using Core.Dtos;
using Core.EncapsulationDtos;
using Core.Model;
using Core.Services;
using E_Ticaret.Extenion;
using E_Ticaret.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IEmailService _emailService;

        private readonly IBasketService _basketService;

        private readonly IMainPageService _mainPageService;

        private readonly RoleManager<AppRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService, IMainPageService mainPageService, RoleManager<AppRole> roleManager, IBasketService basketService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mainPageService = mainPageService;
            _roleManager = roleManager;
            _basketService = basketService;
        }

        public PartialViewResult PartialViewNavbar()
        {
            return PartialView();
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var mainPage = await _mainPageService.GetAllAsync();

            var model = mainPage.ToPagedList(page, 12);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            AppUser hasUser = null;

            if (model.Email != null)
            {
                hasUser = await _userManager.FindByEmailAsync(model.Email);
            }

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email Veya Şifre Yanlış....");

                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResualt;

            if (model.Password != null)
            {
                signInResualt = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RemmemberMe, true);
            }
            else
            {
                return View();
            }

            if (signInResualt.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (signInResualt.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "3 Dakkika Boyunca Giriş Yapamazsınız...." });
                return View();
            }

            ModelState.AddModelErrorList(new List<string>() { "Email Veya Şifre Yanlış.... ", $"Başarısız Giriş Sayısı = {await _userManager.GetAccessFailedCountAsync(hasUser)}" });

            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string chars = "abcçdefgğhiıjklmnoöprsştuüvyz";

            var random = new Random();

            var randomString = new string(Enumerable.Repeat(chars, 40).Select(s => s[random.Next(s.Length)]).ToArray());

            AppUser appUser = new() { UserName = randomString, PhoneNumber = request.Phone, Email = request.Email, UserNameMain = request.UserNameMain };

            var identityResult = await _userManager.CreateAsync(appUser, request.PasswordConfirm);

            var identityResultAddRole = await _userManager.AddToRoleAsync(appUser, "Member");

            if (identityResult.Succeeded)
            {

                TempData["SuccededMessage"] = "Üyelik Kayıt İşlemi Başarılı....";

                return RedirectToAction(nameof(HomeController.SignUp));

            }

            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());

            ModelState.AddModelErrorList(identityResultAddRole.Errors.Select(x => x.Description).ToList());

            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto request)
        {

            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Bu E Mail Adresine Sahip Kulanıcı Bulunamamıştır....");

                return View();
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            var passwordResetLink = Url.Action("ResetPassword", "Home", new { userId = hasUser.Id, Token = passwordResetToken }, HttpContext.Request.Scheme);

            await _emailService.SendResetPasswordEmail(passwordResetLink!, hasUser.Email!);

            TempData["success"] = "Şifre Yenileme Linki E Maili Adresinize Gönderilmiştir";

            return RedirectToAction(nameof(ForgetPassword));
        }

        public async Task<IActionResult> AssignRolesToUser(string id)
        {
            var currentUser = await _userManager.FindByEmailAsync(id);

            ViewBag.userId = currentUser.Id;

            var roles = await _roleManager.Roles.ToListAsync();

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var roleViewModelList = new List<AssignRoleToUserDto>();

            foreach (var role in roles)
            {

                var assignRoleToUserViewModel = new AssignRoleToUserDto()
                {
                    Id = role.Id,

                    Name = role.Name,

                };

                if (userRoles.Contains(role.Name))
                {
                    assignRoleToUserViewModel.Exist = true;
                }

                roleViewModelList.Add(assignRoleToUserViewModel);
            }

            return View(roleViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRolesToUser(string userId, List<AssignRoleToUserDto> requestList)
        {
            var userToAssignRoles = await _userManager.FindByIdAsync(userId);

            foreach (var role in requestList)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(userToAssignRoles, role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userToAssignRoles, role.Name);
                }
            }

            return RedirectToAction("UserList", "Member");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var productWithEmail = _basketService.GetAllWithEmail(user.Email);

            _basketService.DeleteBasketProducts(productWithEmail);

            return RedirectToAction("Index", "Home");

        }
    }
}
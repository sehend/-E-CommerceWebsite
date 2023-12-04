using Core.Dtos;
using Core.Model;
using Core.Services;
using E_Ticaret.Extenion;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class MemberController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;

            _signInManager = signInManager;
        }

        public async Task<IActionResult> UserList()
        {
            var userList = await _userManager.Users.ToListAsync();

            var userViewModelList = userList.Select(x => new UserDto()
            {
                UserNameMain = x.UserNameMain,

                Email = x.Email,

                PhoneNumber = x.PhoneNumber

            }).ToList();

            return View(userViewModelList);
        }

        public async Task<IActionResult> Remove(string id)
        {
            var user = await _userManager.FindByEmailAsync(id);

            await _userManager.DeleteAsync(user!);

            return RedirectToAction(nameof(MemberController.UserList));

        }

        public async Task<IActionResult> UserEdit(string id)
        {
            var onlyOneData = await _userManager.FindByEmailAsync(id);

            var model = new UserDto()
            {
                UserNameMain = onlyOneData.UserNameMain,

                Email = onlyOneData.Email,

                PhoneNumber = onlyOneData.PhoneNumber,


            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDto request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByEmailAsync(request.Email);

            currentUser.UserNameMain = request.UserNameMain;

            currentUser.Email = request.Email;

            currentUser.PhoneNumber = request.PhoneNumber;

            var updateToUserResualt = await _userManager.UpdateAsync(currentUser);

            if (!updateToUserResualt.Succeeded)
            {
                ModelState.AddModelErrorList(updateToUserResualt.Errors);
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);

            await _signInManager.SignOutAsync();


            TempData["SuccededMessage"] = "Üye Bilgileri Başarı ile Degiştirilmiştir....";


            var userEditViewModel = new UserDto()
            {
                UserNameMain = currentUser.UserName,

                Email = currentUser.Email,

                PhoneNumber = currentUser.PhoneNumber,

            };


            return RedirectToAction(nameof(MemberController.UserList));
        }
    }
}

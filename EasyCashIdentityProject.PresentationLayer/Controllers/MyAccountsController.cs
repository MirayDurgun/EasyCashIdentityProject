using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    [Authorize] //login işlemini zorunlu kıldık
    public class MyAccountsController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public MyAccountsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            //await asenkron yapılar içerisinde aynı anda birden çok işlem yapmaya olanak sağlar

            AppUserEditDto appUserEditDto = new AppUserEditDto();
            appUserEditDto.Name = values.Name; //valuesten gelen nameyi ata
            appUserEditDto.Surname = values.Surname;
            appUserEditDto.PhoneNumber = values.PhoneNumber;
            appUserEditDto.Email = values.Email;
            appUserEditDto.City = values.City;
            appUserEditDto.District = values.District;
            appUserEditDto.ImageUrl = values.ImageUrl;
            return View(appUserEditDto);
        }
    }
}

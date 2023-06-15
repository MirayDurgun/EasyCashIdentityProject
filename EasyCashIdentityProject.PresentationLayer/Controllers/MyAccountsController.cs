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
		[HttpPost]
		//Bilgileri güncelleme işlemi
		public async Task<IActionResult> Index(AppUserEditDto appUserEditDto)
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name); //kullanıcı adına göre kullanıcıyı bul

			if (appUserEditDto.Password == appUserEditDto.ConfirmPassword)
			//Eğer dtodan gelen şifre kontrol şifresiyle uyuyorsa aşağıdaki işlemleri yap
			{
				//Güncellenmesini istediğimiz bilgiler
				user.Name = appUserEditDto.Name;
				user.Surname = appUserEditDto.Surname;
				user.Email = appUserEditDto.Email;
				user.PhoneNumber = appUserEditDto.PhoneNumber;
				user.City = appUserEditDto.City;
				user.District = appUserEditDto.District;
				user.ImageUrl = "Test";
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEditDto.Password);
				//kullanıcının şifresini
				//userdan gelen bilgileri dtoya göre atar
				//bu ifade sayesinde şifreyi veritabanına şifreli olarak göndermiş oluruz yani datadan şifreyi göremeyiz
				var result = await _userManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Login");
				}

			}
			return View();
		}
	}
}

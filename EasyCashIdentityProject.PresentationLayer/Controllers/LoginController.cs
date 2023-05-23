using EasyCashIdentityProject.EntityLayer.Concrete;
using EasyCashIdentityProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			this._userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginViewModel loginViewModel)
		{
			var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
			//overload olarak username, şifre, tarayıca şifrenin hatırlanıp hatırlanmayacağını soruyor biz false dedik 
			//ve kullanıcı sisteme otantike olurken her yanlış girdiğinde datada field değeri 1 artar, 5 kez yanlış girerse
			//sisteme bir süre giremez. sonuncu da hata işlemi aktif olsun mu olmasın mı diye sorar buna da false dedik aktif olmasın
			//PasswordSignInAsync istediği tüm parametreleri girdik 4 tane girmezsek hata verir

			if (result.Succeeded) //kullanıcı giriş yaptığında
			{
				var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
				//FindByNameAsync bir kullanıcı bulur ve döner

				if (user.EmailConfirmed == true)//eğer mail adresi onaylandıysa
				{
					return RedirectToAction("Index", "MyProfile"); //kullanıcı giriş yapınca profiline gitsin
				}

			}

			return View();
		}

	}
}

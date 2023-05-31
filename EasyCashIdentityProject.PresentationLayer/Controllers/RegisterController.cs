using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class RegisterController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public RegisterController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
			//solidi ezmemek adına
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
		//parantezin içinde appuser çağırırsak ihtiyacımı olmayan
		//sütunları da kullanmak durumunda kalırız
		//bu nedenle bize lazım olacakları yazdığımız
		//dto katmanında oluşturduğumuz sınıfı kullanırız
		{
			if (ModelState.IsValid) //eğer modelstate geçerliyse buradaki işlemler gerçekleşir
									//yani fluentvalidationdan geçmesi
			{
				Random random = new Random();
				//random kod oluştur
				int code;
				code = random.Next(10000, 1000000);
				AppUser appUser = new AppUser()
				{
					UserName = appUserRegisterDto.Username,
					Name = appUserRegisterDto.Name,
					Surname = appUserRegisterDto.Surname,
					Email = appUserRegisterDto.Email,
					City = "İstanbul",
					District = "B",
					ImageUrl = "C",
					ConfirmCode = code
					//kod doğrulama işlemi
				};

				var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);
				//await yazdığımız an metot async olur
				//CreateAsync identity için veri ekleme işlemi sağlar

				if (result.Succeeded)
				{
					//mail doğrulama
					MimeMessage mimeMessage = new MimeMessage();
					MailboxAddress mailboxAddressFrom = new MailboxAddress("Easy Cash", "miraydurgun67@gmail.com");//gönderen
					MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email); //kullanıcının mail adresine gönder
					mimeMessage.From.Add(mailboxAddressFrom);//mail kimden gidecek
					mimeMessage.To.Add(mailboxAddressTo);//kime gidecek

					var bodyBuilder = new BodyBuilder();
					bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz :" + code;
					mimeMessage.Body = bodyBuilder.ToMessageBody();//bodybuilderdan gelen tüm mesajı bodye ekle

					mimeMessage.Subject = "Easy Cash Onay Kodu";//konu
					SmtpClient client = new SmtpClient();
					client.Connect("smtp.gmail.com", 587, false);//bağlantı kur
																 //587 türkiyedeki port numarası, güvenlik sağlansın diyede false dedik
					client.Authenticate("miraydurgun67@gmail.com", "hfdywsspatuozwvt");//kullanıcı adı ve şifre
																	   //uygulama şifresi için mailde telefon bilgisi ve iki adımlı doğrulama açık olmalı
																	   //sonrasında hesabı yönet
																	   //uygulama şifreleri kısmından özel diyerek şifre oluşturup buraya kopyalıyoruz
					client.Send(mimeMessage);//mimemessageden gelen değeri gönder
					client.Disconnect(true);//başarılı bir şekilde çıkışını yap


					TempData["Mail"] = appUserRegisterDto.Email;//viewe veri taşımak için
																//mail ismindeki bir string içinde dtodan gelen mail değerini sakladık

					return RedirectToAction("Index", "ConfirmMail");
					//ConfirmMail maile kod yollayacağız kayıt olurken kullanıcıya maille kod gidecek
					//doğrulama kodu olacak
				}

				else
				{
					//index'te kayıt olurken kuralları/hataları 
					//yazdırmadığı için else ve foreach açtık
					//sebebi result succeeded dönmediği için direkt return view'e gidiyordu

					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
						//bu ifade resulttan gelen hataları okur ve 
						//modelstatenin içine ekler
						//kısaca hataları getirir.
					}
				}
			}
			return View();
		}
	}
}

/*
 * Şifre
 * En az 6 karakterden oluşacak
 * En az bir küçük harf
 * En az bir büyük harf
 * En az bir sembol
 * En az bir sayı içermeli
 */
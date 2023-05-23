﻿using EasyCashIdentityProject.EntityLayer.Concrete;
using EasyCashIdentityProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class ConfirmMailController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		public ConfirmMailController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var value = TempData["Mail"];
			ViewBag.Mail = TempData["Mail"];
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)//bu indexi modele bağlıyoruz
		{
			var user = await _userManager.FindByEmailAsync(confirmMailViewModel.Mail);
			//FindByEmailAsync email bulur ve döner

			if (user.ConfirmCode == confirmMailViewModel.ConfirmCode)
			{
				user.EmailConfirmed = true;
				await _userManager.UpdateAsync(user); //EmailConfirmedin true ya döndüğünü kaydetmesi için kullandık
				return RedirectToAction("Index", "Login");
			}

			return View();
		}
	}
}

// Kullanı Adı - Şifre ile eşleşmeli < = > Email Confirmed olmalı

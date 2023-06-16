using EasyCashIdentityProject.BusinessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.DtoLayer.Dtos.CustomerAccountProcessDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class SendMoneyController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ICustomerAccountProcessService _customerAccountProcessService;

		public SendMoneyController(UserManager<AppUser> userManager, ICustomerAccountProcessService customerAccountProcessService)
		{
			_userManager = userManager;
			_customerAccountProcessService = customerAccountProcessService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(SendMoneyForCustomerAccountProcessDTO sendMoneyForCustomerAccountProcessDTO)
		{
			var context = new Context();
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			//await asenkron yapılar içerisinde aynı anda birden çok işlem yapmaya olanak sağlar

			sendMoneyForCustomerAccountProcessDTO.SenderID = user.Id;
			sendMoneyForCustomerAccountProcessDTO.ProcessDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
			sendMoneyForCustomerAccountProcessDTO.ProcessType = "Havale";

			var receiverAccountNumberID = context.CustomerAccounts.Where(x => x.CustomerAccountNumber == sendMoneyForCustomerAccountProcessDTO.ReceiverAccountNumber).Select(y => y.CustomerAccountID).FirstOrDefault();
			sendMoneyForCustomerAccountProcessDTO.ReceiverID = receiverAccountNumberID;

			//_customerAccountProcessService.TInsert();

			return RedirectToAction("Index", "Deneme");
		}
	}
}

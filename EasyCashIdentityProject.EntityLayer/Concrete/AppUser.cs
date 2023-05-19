using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
		//IdentityUser
		//asp net user sınıfı ile ilişkili hale getiriyoruz
		//app user sınıfını, aspnet user sınıfı ile birlikte kullanabilmek için yazılır
		public string Name { get; set; }
        public string Surname { get; set; }
        public string District { get; set; } //ilçe
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public int ConfirmCode { get; set; }

        public List<CustomerAccount> CustomerAccounts { get; set; }
    }
}

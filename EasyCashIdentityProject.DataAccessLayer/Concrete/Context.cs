﻿using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DataAccessLayer.Concrete
{
	public class Context : IdentityDbContext<AppUser, AppRole, int>

	//IdentityDbContext'in detaylarına gidince (sağ tık Go To definition) 
	//DbContext'ten miras alır. Altında DbContext sınıfı mevcut.
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLOCALDB; initial catalog=EasyCashDB; integrated Security=true;");
		}

		public DbSet<CustomerAccount> CustomerAccounts { get; set; }
		public DbSet<CustomerAccountProcess> CustomerAccountsProcess { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<CustomerAccountProcess>()
				.HasOne(x => x.SenderCustomer)
				.WithMany(y => y.CustomerSender)
				.HasForeignKey(z => z.SenderID)
				.OnDelete(DeleteBehavior.ClientSetNull);

			builder.Entity<CustomerAccountProcess>()
				.HasOne(x => x.ReceiverCustomer)
				.WithMany(y => y.CustomerReceiver)
				.HasForeignKey(z => z.ReceiverID)
				.OnDelete(DeleteBehavior.ClientSetNull);

			base.OnModelCreating(builder);
		}
	}
}

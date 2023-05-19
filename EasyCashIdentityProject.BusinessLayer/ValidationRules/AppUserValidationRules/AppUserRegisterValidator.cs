using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.ValidationRules.AppUserValidationRules
{
    public class AppUserRegisterValidator : AbstractValidator<AppUserRegisterDto>
    {
        public AppUserRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez.");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("İsim alanı maksimum 30 karakter olabilir.");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("İsim alanı minimum 2 karakter olabilir.");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyisim alanı boş geçilemez.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta alanı boş geçilemez.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
			//mail biçiminde girilip girilmediğini EmailAddress kontrol eder

			RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adı alanı boş geçilemez.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez.");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrar alanı boş geçilemez.");
            RuleFor(x => x.ConfirmPassword).Equal(y => y.Password).WithMessage("Şifreler eşleşmiyor.");
			//equal eşleşmeyi kontrol eder
		}
	}
}

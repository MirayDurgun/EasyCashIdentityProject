using Microsoft.AspNetCore.Identity;

namespace EasyCashIdentityProject.PresentationLayer.Models
{
    public class CustomIdentityValidator : IdentityErrorDescriber
    {
		//bu kısımda kayıt ol sayfasında yabancı dille
		//yazılan hataları türkçeleştireceğiz
		public override IdentityError PasswordTooShort(int length)
		//ovveride metodun işleyişi bozulmadan
		//işleyişi bizim istediğimiz formata çevirir
		{
			return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Şifre en az {length} karakter olmalıdır."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresUpper",
                Description = "Şifre en az bir büyük harf içermelidir."
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresLower",
                Description = "Şifre en az bir küçük harf içermelidir."
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresDigit",
                Description = "Şifre en az bir rakam içermelidir."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Şifre en az bir alfanümerik olmayan karakter içermelidir."
            };
        }
    }
}

/*
  IdentityError seçenekleri şunlardır:

DefaultError: Bu, IdentityError'un varsayılan hatasıdır. Kimlik doğrulama hatası oluştuğunda bu mesaj kullanıcılara gösterilir.

DuplicateEmail: Kullanıcı kaydı sırasında e-posta adresi zaten kayıtlıysa bu hata oluşur.

DuplicateUserName: Kullanıcı adı zaten mevcut olduğunda bu hata oluşur.

InvalidEmail: Geçersiz bir e-posta adresi girildiğinde bu hata oluşur.

InvalidUserName: Geçersiz bir kullanıcı adı girildiğinde bu hata oluşur.

PasswordMismatch: Kullanıcının girdiği şifreler eşleşmiyorsa bu hata oluşur.

PasswordRequiresDigit: Şifre en az bir rakam içermelidir.

PasswordRequiresLower: Şifre en az bir küçük harf içermelidir.

PasswordRequiresNonAlphanumeric: Şifre en az bir alfanümerik olmayan karakter içermelidir.

PasswordRequiresUpper: Şifre en az bir büyük harf içermelidir.

PasswordTooShort: Şifre belirlenen minimum uzunlukta değilse bu hata oluşur.
 */

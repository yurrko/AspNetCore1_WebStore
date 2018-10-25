using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Models.Account
{
    public class RegisterUserViewModel
    {
        //[Display( Name = "Логин" )]
        [Required, MaxLength( 256 )]
        public string UserName { get; set; }

        //[Display( Name = "Пароль" )]
        [Required, DataType( DataType.Password )]
        public string Password { get; set; }

        //[Display( Name = "Подтверждение пароля" )]
        [DataType( DataType.Password ), Compare( nameof( Password ) )]
        public string ConfirmPassword { get; set; }

        //[Display( Name = "Почтовый ящик" )]
        [Required, DataType( DataType.EmailAddress )]
        public string Email { get; set; }

        //[Display( Name = "Подтверждение почтового адреса" )]
        [Required, DataType( DataType.EmailAddress ), Compare( nameof( Email ) )]
        public string ConfirmEmail { get; set; }
    }
}
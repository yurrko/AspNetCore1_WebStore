using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.Models.Account;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController( UserManager<User> userManager, SignInManager<User> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login( string returnUrl )
        {
            return View( new LoginViewModel()
            {
                ReturnUrl = returnUrl
            } );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login( LoginViewModel model )
        {
            if ( ModelState.IsValid )
            {
                var loginResult = await _signInManager.PasswordSignInAsync( model.UserName,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false ); //Проверяем логин/пароль пользователя\

                if ( loginResult.Succeeded ) //если проверка успешна
                {
                    if ( Url.IsLocalUrl( model.ReturnUrl ) ) //и returnUrl - локальный
                    {
                        return Redirect( model.ReturnUrl ); //перенаправляем туда откуда пришли
                    }
                    return RedirectToAction( "Index", "Home" ); //иначе на главную
                }
            }
            //говорим пользователю что вход невозможен
            ModelState.AddModelError( "", "Вход невозможен" );

            return View( model );
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View( new RegisterUserViewModel() );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( RegisterUserViewModel model )
        {
            if ( ModelState.IsValid )
            {
                //создаем сущность пользователь
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                // используем менеджер для создания
                var createResult = await _userManager.CreateAsync( user, model.Password );
                if ( createResult.Succeeded )
                {
                    //await _userManager.SetEmailAsync( user, model.Email );
                    //если успешно - производим логин
                    await _signInManager.SignInAsync( user, false );
                    await _userManager.AddToRoleAsync( user, "User" );
                    return RedirectToAction( "Index", "Home" );
                }
                else //иначе
                {
                    //выводим ошибки
                    foreach ( var identityError in createResult.Errors )
                    {
                        ModelState.AddModelError( "Ошибка", identityError.Description );
                    }
                }
            }
            return View( model );
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction( "Index", "Home" );
        }
    }
}
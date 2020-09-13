using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using shopapp.Common.Dto;
using shopapp.Common.Extensions;
using shopapp.Domain.Entities;
using shopapp.Web.Identity;
using shopapp.WebUI.Controllers;
using shoppapp.Services.Abstract;

namespace shopapp.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : BaseController
    {
        private UserManager<ApplicationUser> _userManager;//kullanıcı oluşturulurken veya kullanıcı ile ilgili işlemler yapılırken kullanılır
        private SignInManager<ApplicationUser> _signInManager;//login işlemlerinde kullanılıyor.
        private IEmailSender _emailSender;
        private ICartService _cartService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cartService = cartService;
        }

        public IActionResult Register()
        {
            return View(new RegisterUserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = code
                });

                // send email
                await _emailSender.SendEmailAsync(model.Email, "Hesabınızı Onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:56916{callbackUrl}'>tıklayınız.</a>");

                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hesap Onayı",
                    Message = "Email adresinize gönderilen link ile hesabınızı onaylayınız.",
                    Css = "warning"
                });

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Bilinmeyen bir hata oluştu lütfen tekrar deneyiniz.");

            return View(model);

        }

        public IActionResult Login()
        {
            return View(new LoginUserModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model, string returnUrl = null)
        {

            returnUrl = returnUrl ?? "~/"; //returnUrl varsa alalım yoksa anadizine yönlendirelim. Yetkisinin olmadığı sayfayı takip edebilmek için.

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı ile daha önce hesap oluşturulmamıştır");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen hesabınızı mail ile onaylayınız");
                return View(model);
            }


            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();


            TempData.Put("message", new ResultMessage()
            {
                Title = "Oturum Kapatıldı",
                Message = "Hesabınız güvenli bir şekilde sonlandırıldı.",
                Css = "warning"
            });


            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {

                TempData.Put("message", new ResultMessage()
                {
                    Title = "Hesap Onayı",
                    Message = "Hesap onayı için bilgileriniz yanlış",
                    Css = "danger"
                });

                return Redirect("~/");

            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {

                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    //create cart object
                    _cartService.InitializeCart(user.Id); ;//her girin kullanıcı için bir cart açıyorum.
                    _cartService.Save();
                    TempData.Put("message", new ResultMessage()
                    {
                        Title = "Hesap Onayı",
                        Message = "Hesabınız başarılı ile onaylanmıştır.",
                        Css = "success"
                    });
                    return RedirectToAction("Login");
                }

            }
            TempData.Put("message", new ResultMessage()
            {
                Title = "Hesap Onayı",
                Message = "Hesabınız onaylanmadı.",
                Css = "danger"
            });
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Forgot Password",
                    Message = "Bilgileriniz hatalıdır.",
                    Css = "danger"
                });
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                TempData.Put("message", new ResultMessage()
                {
                    Title = "Forgot Password",
                    Message = "Email adresi ile bir kullanıcı bulunamadı.",
                    Css = "danger"
                });
                return View();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);//kullanıcı için bir token oluşturuyor password reset işlemi sırasında bu token'ı kullanıyor.
            var callbackUrl = Url.Action("ResetPassword", "Account", new
            {
                token = code
            });

            // send email
            await _emailSender.SendEmailAsync(Email, "Reset Password", $"Parolanızı yenilemek için linke <a href='http://localhost:56916{callbackUrl}'>tıklayınız.</a>");

            TempData.Put("message", new ResultMessage()
            {
                Title = "Forgot Password",
                Message = "Parola yenilemek için hesabınıza mail gönderildi.",
                Css = "warning"
            });

            return RedirectToAction("Login", "Account");

        }

        public IActionResult ResetPassword(string token)
        {
            if (token == null)
            {
                return RedirectToAction("Home", "Index");
            }

            var model = new CustomResetPasswordModel { Token = token };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(CustomResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Home", "Index");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }
    }
}

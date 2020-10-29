using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BaseProjectWebRazor.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BaseProjectWebRazor.Areas.Admin.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration configuration;
        public LoginModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = configuration.GetSection("SiteUser").Get<SiteUser>();

            if (UserName == user.UserName)
            {
                var passwordHasher = new PasswordHasher<string>();
                if (passwordHasher.VerifyHashedPassword("admin", user.Password, Password) == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, UserName)
                        };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToPage("/Index");
                }
            }
            UserName = "";
            Password = "";
            Message = "Tài khoản hoặc mật khẩu không đúng";
            return Page();
        }
        //public async Task<IActionResult> OnPost()
        //{
        //    var user = configuration.GetSection("SiteUser").Get<SiteUser>();

        //    if (UserName == user.UserName)
        //    {
        //        var passwordHasher = new PasswordHasher<string>();
        //        if (passwordHasher.VerifyHashedPassword("admin", user.Password, Password) == PasswordVerificationResult.Success)
        //        {
        //            var claims = new List<Claim>
        //            {
        //                new Claim(ClaimTypes.Name, UserName)
        //            };
        //            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        //            return RedirectToPage("/admin/index");
        //        }
        //    }
        //    Message = "Invalid attempt";
        //    return Page();
        //}
    }
}

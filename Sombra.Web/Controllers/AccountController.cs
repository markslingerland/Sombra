using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sombra.Web.Infrastructure.Extensions;
using Sombra.Web.Models;
using Sombra.Web.Services;
using Sombra.Web.ViewModels;

namespace Sombra.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userManager;
        
        public AccountController(UserService userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("wachtwoord-vergeten")]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost("wachtwoord-vergeten")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
             if (ModelState.IsValid){
                try 
                {
                    var response = await _userManager.ForgotPassword(forgotPasswordViewModel);
                    return this.ReturnToHome();
                } catch(Exception x){
                    ModelState.AddModelError("ChangePasswordError", x.ToString()); // TODO: Replace x with your error message

                }
            }   
            return View(forgotPasswordViewModel);
        }

        [HttpGet("wachtwoord-wijzigen")]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [HttpPost("wachtwoord-wijzigen")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel, string securityToken)
        {
            if (ModelState.IsValid){
                try 
                {
                    var response = await _userManager.ChangePassword(changePasswordViewModel, securityToken);
                    return this.ReturnToHome();
                } catch(Exception x){
                    ModelState.AddModelError("ChangePasswordError", x.ToString()); // TODO: Replace x with your error message
                }
            }
            return View(changePasswordViewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

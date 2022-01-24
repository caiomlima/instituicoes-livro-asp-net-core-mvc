using Capitulo2.Models.Infra;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capitulo2.Controllers {
    [Authorize]
    public class InfraController : Controller {

        private readonly UserManager<UsuarioDoApp> _userManager;
        private readonly SignInManager<UsuarioDoApp> _signInManager;
        private readonly ILogger _logger;

        public InfraController(UserManager<UsuarioDoApp> userManager, SignInManager<UsuarioDoApp> signInManager, ILogger<InfraController> logger) {
            _userManager = userManager; 
            _signInManager = signInManager;
            _logger = logger;
        }

        // GET e POST Acessar - Login
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Acessar(string returnUrl = null) {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Acessar(AcessarViewModel model, string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;
            if(ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, model.LembrarDeMim, lockoutOnFailure: false);
                if(result.Succeeded) {
                    _logger.LogInformation("Usuário autenticado");
                    //return RedirectToAction(returnUrl);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Falha na tentativa de login");
            return View(model);
        }


        // GET Sar - Logout
        [HttpGet]
        public async Task<IActionResult> Sair() {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Usuário realizou logout");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        // GET e POST Registrar - Cadastro
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegistrarNovoUsuario(string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarNovoUsuario(RegistrarNovoUsuarioViewModel model, string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid) {
                var user = new UsuarioDoApp {
                    UserName = model.Email, Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    _logger.LogInformation("Usuário	criou uma nova conta com senha.");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _signInManager.SignInAsync(user, isPersistent:false);
                    _logger.LogInformation("Usuário	acesso com a conta criada.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }


        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            } else {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

    }
}

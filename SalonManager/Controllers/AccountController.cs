using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonManager.Data.Context;
using SalonManager.Web.ViewModels;
using System.Security.Claims;
using SalonManager.Services.Services.Users;

namespace SalonManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUserServices _userServices;

        public AccountController(AppDbContext context, IUserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.PasswordHash == model.Password);

                if (user != null)
                {
                    // Autenticar o usuário
                    var claims = new[]
                    {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "Member")
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Credenciais inválidas.";
                    return View(model);
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existUsername = await _context.Users.Where(ex => ex.Username == model.Username).FirstOrDefaultAsync();

                    if (existUsername == null)
                    {

                        var user = new UserRequest
                        {
                            Username = model.Username,
                            Email = model.Email,
                            PasswordHash = model.Password,
                            IsAdmin = false
                        };
                        await _userServices.CreateUser(user);
                        ViewData["ErrorMessage"] = "Usuario cadastrada com sucesso";
                        return View("Login");
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Usuario já existe, tente outro nome!";
                        return View("Registrar");
                    }

                }
                else
                {
                    ViewData["ErrorMessage"] = "Usuario faltando informações";
                    return View("Login");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IActionResult AccessDenied()
        {
            ViewData["ErrorMessage"] = "Você não tem permissão para acessar esta página(Apenas administradores).";
            return View("Login");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}

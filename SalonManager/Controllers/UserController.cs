using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces;
using SalonManager.Services.Services.Users;
using SalonManager.Web.Filters;
using SalonManager.Web.ViewModels;

namespace SalonManager.Web.Controllers
{
    [AdminAuthorization]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userServices.GetAll();
            var userViewModels = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                IsAdmin = u.IsAdmin
            }).ToList();
            return View(userViewModels);
        }

        public IActionResult Create()
        {
            var model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new UserRequest
                    {
                        Username = model.Username,
                        Email = model.Email,
                        PasswordHash = model.PasswordHash,
                        IsAdmin = model.IsAdmin
                    };
                    await _userServices.CreateUser(user);
                    ViewData["MensagemSucesso"] = "Usuario cadastrada com sucesso";
                    return View("Index");
                }
                ViewData["MensagemErro"] = "Usuario faltando informações";
                return View();
            }
            catch (Exception ex)
            {
                ViewData["MensagemErro"] = $"Erro ao cadastrar o usuario, mensagem apresentada: {ex.Message}";
                return View("Index");
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userServices.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                IsAdmin = user.IsAdmin

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new UserRequest
                    {
                        Username = model.Username,
                        Email = model.Email,
                        PasswordHash = model.PasswordHash,
                        IsAdmin = model.IsAdmin
                    };
                    await _userServices.UpdateUser(id, user);

                    ViewData["MensagemSucesso"] = "Usuario editada com sucesso! ";
                    return View("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Erro ao alterar o usuario, mensagem apresentada: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userServices.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userServices.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCofirmed(UserViewModel model)
        {
            try
            {
                await _userServices.Delete(model.Id);
                TempData["MensagemSucesso"] = "Usuario apagada com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["MensagemErro"] = $"Erro ao apagar o usuario, mensagem apresentada: {ex.Message}";
                return RedirectToAction("Index");
            }

        }
    }
}

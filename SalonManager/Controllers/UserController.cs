using Microsoft.AspNetCore.Mvc;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces;
using SalonManager.Services.Services.Users;
using SalonManager.Web.ViewModels;

namespace SalonManager.Web.Controllers
{
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
                Email = u.Email
            }).ToList();
            return View(userViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]        
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserRequest
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = model.PasswordHash // Você deve hashear a senha aqui
                };
                await _userServices.CreateUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
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
                PasswordHash = user.PasswordHash
                
            };
            return View(model);
        }

        [HttpPost]        
        public async Task<IActionResult> Edit(int id, UserViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var user = new UserRequest
                {                    
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = model.PasswordHash // Atualize conforme necessário
                };
                await _userServices.UpdateUser(id, user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
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
                Email = user.Email
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]        
        public async Task<IActionResult> DeleteCofirmed(UserViewModel model)
        {
            try
            {
                await _userServices.Delete(model.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
          
        }
    }
}

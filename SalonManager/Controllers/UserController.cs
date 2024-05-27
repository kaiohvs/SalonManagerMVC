using Microsoft.AspNetCore.Mvc;
using SalonManager.Domain.Interfaces;
using SalonManager.Services.Services.Users;

namespace SalonManager.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> Create([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna detalhes dos erros de validação
            }

            try
            {
                var data = await _userServices.CreateUser(request);

                return Ok(data);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> Update(int id, [FromBody] UserRequest request)
        {
            try
            {
                var data = await _userServices.UpdateUser(id, request);

                return Ok(data);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetAll()
        {
            try
            {
                var data = await _userServices.GetAll();

                return Ok(data);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetById(int id)
        {
            try
            {
                var data = await _userServices.GetById(id);

                return Ok(data);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                var removido = await _userServices.Delete(id);

                if (removido)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

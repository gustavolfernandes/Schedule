using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Schedule.domain.Interfaces.Repositories;
using Schedule.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Schedule.Controllers
{
    ////https://localhost:5001/contacts
    [Route("contacts")]
    public class ContactController : Controller
    {
        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(
            int id,
            [FromServices] IContactRepository repository)
        {
            try
            {
                var contacts = await repository.GetById(id);
                return Ok(contacts);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível encontrar contato" });
            }
        }
        [HttpGet]
        [Route("users/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByUserId(
            [FromServices] IContactRepository repository,
            int id)
        {
            var result = await repository.GetByUserId(id);
            return Ok(result);
        }
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Post(
            [FromBody] Contact model,
            [FromServices] IContactRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await repository.Post(model);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Não foi possível criar o contato" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar contato" });
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(
            int id,
            [FromBody] Contact model,
            [FromServices] IContactRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await repository.Put(id, model);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Não foi possível atualizar o contato" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível atualizar o contato" });
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] IContactRepository repository)       
            {
            try { 
                var result = await repository.Delete(id);
                return Ok(new { message = "Contato removido." });
            }
            catch
            {
                    return BadRequest(new { message = "Não foi possível remover a contato" });
            }
        }
        
    }
}
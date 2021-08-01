using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Schedule.domain.Interfaces.Repositories;
using Schedule.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;

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
                return Ok(await repository.GetById(id));
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
        [Route("register")]
        [Authorize]
        public async Task<IActionResult> Post(
            [FromBody] Contact model,
            [FromServices] IContactRepository repository)
        {
            if (!ModelState.IsValid)
               return BadRequest(new { message = "Usuário invalido." });
            try
            {
                return Ok(await repository.Post(model));
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Contato em conflito durante alteração." });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar contato." });
            }
        }
        [HttpPut]
        [Route("update/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(
            int id,
            [FromBody] Contact model,
            [FromServices] IContactRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            try
            {
                return Ok(await repository.Put(id, model));
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Não foi possível atualizar o contato" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                return Ok(await repository.Delete(id));
            }
            catch
            {
                    return BadRequest(new { message = "Não foi possível remover a contato" });
            }
        }
        
    }
}
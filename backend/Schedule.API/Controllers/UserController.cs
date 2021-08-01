using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Schedule.domain.Entities;
using Schedule.Data;
using Schedule.domain.Helpers;
using Schedule.Application.Services;
using Schedule.domain.Interfaces.Repositories;

namespace Schedule.Controllers
{
    //https://localhost:5001/users
    [Route("users")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]

        //Lista de usuários
        public async Task<IActionResult>Get([FromServices] IUserRepository repository)
        {
            try {
            var result = await repository.Get();
            return Ok(result);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possivel encontrar usuários." });
            }
        }
        //Procura user por Id
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> GetById(
            int id,
            [FromServices] IUserRepository repository)
        {
            try
            {
                return await repository.GetById(id);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível encontrar usuário." });
            }         
        }

        //Registra user
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Post(
            [FromBody] User model,
            [FromServices] IUserRepository repository)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await repository.Post(model);
                if (!(result == null))
                {
                    return Ok(result);
                }
                else {
                    return Conflict(new { message = "Usuário já existe." });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar usuário." });
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate(
                    [FromServices] IUserRepository repository,
                    [FromBody] User model)
        {
            if (model == null)
                return NotFound(new { message = "Usuário ou senha inválidos." });
            try
            {
                return Ok(await repository.Authenticate(model));
            }
            catch(Exception)
            {
                    return BadRequest(new { message = "Usuário ou senha inválidos." });               
            }
        }
    }
}

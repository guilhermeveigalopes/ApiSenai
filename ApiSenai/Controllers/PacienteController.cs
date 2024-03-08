using ApiSenai.Data;
using ApiSenai.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ApiSenai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public PacienteController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        //Método Listar
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPacient()
        {
            return Ok(new { success = true, data = await _applicationContext.Pacients.ToListAsync() });
        }
        //Método Criar
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePet(Pacient pacient)
        {
            _applicationContext.Pacients.Add(pacient);
            await _applicationContext.SaveChangesAsync();
            return Ok(new { success = true, data = pacient });
        }
        //Método Update
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePet(int id, Pacient pacient)
        {
            var x = await _applicationContext.Pacients.FindAsync(id);
            if (x == null)
            {
                return NotFound();
            }
            x.Nome = pacient.Nome;
            x.Endereco = pacient.Endereco;
            x.Telefone = pacient.Telefone;

            _applicationContext.Pacients.Update(x);
            await _applicationContext.SaveChangesAsync();
            return Ok(new { success = true, data = x });
        }
        //Método Delete

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _applicationContext.Pacients.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _applicationContext.Pacients.Remove(pet);
            await _applicationContext.SaveChangesAsync();

            return Ok(new { success = true });
        }

    }
}

using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Mvc;
using Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IBL_Personas _bl;

        public PersonasController(IBL_Personas bl)
        {
            _bl = bl;
        }

        // GET: api/<PersonasController>
        [ProducesResponseType(typeof(List<Persona>), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bl.Get());
        }

        // GET api/<PersonasController>/documento
        [HttpGet("{documento}")]
        public IActionResult Get(string documento)
        {
            try
            {
                var persona = _bl.Get(documento);
                if (persona != null)
                    return Ok(persona);
                else
                    return NotFound($"Persona con el documento {documento} no encontrada.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<PersonasController>
        [HttpPost]
        public IActionResult Post([FromBody] Persona persona)
        {
            try
            {
                _bl.Insert(persona);
                return CreatedAtAction(nameof(Get), new { documento = persona.Documento }, persona);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PersonasController>/documento
        [HttpPut("{documento}")]
        public IActionResult Put(string documento, [FromBody] Persona persona)
        {
            try
            {
                if (documento != persona.Documento)
                {
                    return BadRequest("El documento proporcionado no coincide con el documento de la persona.");
                }
                _bl.Update(persona);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PersonasController>/documento
        [HttpDelete("{documento}")]
        public IActionResult Delete(string documento)
        {
            try
            {
                _bl.Delete(documento);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

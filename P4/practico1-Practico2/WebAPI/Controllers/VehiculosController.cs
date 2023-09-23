using BusinessLayer.BLs;
using BusinessLayer.IBLs;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly IBL_Vehiculos _bl;

        public VehiculosController(IBL_Vehiculos bl)
        {
            _bl = bl;
        }

        // GET: api/<VehiculosController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bl.Get());
        }

        // GET api/<VehiculosController>/matricula
        [HttpGet("{matricula}")]
        public IActionResult Get(string matricula)
        {
            try
            {
                var vehiculo = _bl.Get(matricula);
                if (vehiculo != null)
                    return Ok(vehiculo);
                else
                    return NotFound($"Vehículo con la matrícula {matricula} no encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<VehiculosController>
        [HttpPost]
        public IActionResult Post([FromBody] Vehiculo vehiculo)
        {
            try
            {
                _bl.Insert(vehiculo, vehiculo.DocumentoPropietario);
                return CreatedAtAction(nameof(Get), new { matricula = vehiculo.Matricula }, vehiculo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<VehiculosController>/matricula
        [HttpPut("{matricula}")]
        public IActionResult Put(string matricula, [FromBody] Vehiculo vehiculo)
        {
            try
            {
                if (matricula != vehiculo.Matricula)
                {
                    return BadRequest("La matrícula proporcionada no coincide con la matrícula del vehículo.");
                }
                _bl.Update(vehiculo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<VehiculosController>/matricula
        [HttpDelete("{matricula}")]
        public IActionResult Delete(string matricula)
        {
            try
            {
                _bl.Delete(matricula);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<VehiculosController>/propietario/{documento}
        [HttpGet("propietario/{documento}")]
        public IActionResult GetVehiculosPorPersona(string documento)
        {
            try
            {
                var vehiculos = _bl.GetByOwnerDocument(documento);
                if (vehiculos != null && vehiculos.Count > 0)
                    return Ok(vehiculos);
                else
                    return NotFound($"No se encontraron vehículos para la persona con documento {documento}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepositorio _villarepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villarepo, IMapper mapper)
        {
            _logger = logger;
            _villarepo= villarepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                _logger.LogInformation("obtener las villas");

                IEnumerable<Villa> Villalist = await _villarepo.Obtenertodos();

                _response.Resultado = _mapper.Map<IEnumerable<VillaDto>>(Villalist);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _response;
           
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("error al traer la villa con id" + id);
                    _response.statusCode= HttpStatusCode.BadRequest;
                    _response.IsExitoso= false;
                    return BadRequest(_response);
                }

                //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
                var villa = await _villarepo.ObteneR(x => x.Id == id);
                if (villa == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<VillaDto>(villa);
                _response.statusCode= HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                 _response.IsExitoso=false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
                return _response;
            }
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> PostVilla([FromBody] VillaCreateDto createDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _villarepo.ObteneR(v => v.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("Nombre existe", "La villa con este nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest();
                }

                Villa model = _mapper.Map<Villa>(createDto);

                await _villarepo.Crear(model);
                model.FechaCreacion= DateTime.Now;
                model.FechaActualizacion= DateTime.Now;
                _response.Resultado = model;
                _response.statusCode -= HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso= false;
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }

            return _response;
            
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public async  Task<IActionResult> DeleteVilla(int id)
        {

            try
            {
                if (id == 0)
                { 
                _response.IsExitoso= false;
                _response.statusCode= HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villa = await _villarepo.ObteneR(x => x.Id == id);
                if (villa == null)
                {
                    _response.IsExitoso= false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _villarepo.Remover(villa);
                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsExitoso= false;
                _response.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return BadRequest(_response);
           
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto UpdateDto)
        {
            if(UpdateDto == null || id!= UpdateDto.Id)
            {
                _response.IsExitoso= false;
                _response.statusCode= HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Villa modelo= _mapper.Map<Villa>(UpdateDto);

            await _villarepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;


            return  Ok(_response);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateParcialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id ==0)
            {
                return BadRequest();
            }
            var villa = await _villarepo.ObteneR( v => v.Id == id, tracked: false );

            VillaUpdateDto villadto = _mapper.Map<VillaUpdateDto>(villa);

            if (villa == null) return BadRequest();

            patchDto.ApplyTo(villadto, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = _mapper.Map<Villa>(villadto);

            await _villarepo.Actualizar(modelo);
            _response.statusCode= HttpStatusCode.NoContent;

            return Ok(_response);
        }
    }
}

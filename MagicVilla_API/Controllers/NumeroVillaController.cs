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
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepositorio _villarepo;
        private readonly INumeroVillaRepositorio _numeroRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroVillaController(ILogger<VillaController> logger, IVillaRepositorio villarepo, INumeroVillaRepositorio numerorepo, IMapper mapper)
        {
            _logger = logger;
            _villarepo= villarepo;
            _numeroRepo= numerorepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas()
        {
            try
            {
                _logger.LogInformation("obtener las villas");

                IEnumerable<NumeroVilla> numeroVillalist = await _numeroRepo.Obtenertodos();

                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroVillalist);
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

        [HttpGet("id:int", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("error al traer numero villa con id" + id);
                    _response.statusCode= HttpStatusCode.BadRequest;
                    _response.IsExitoso= false;
                    return BadRequest(_response);
                }

                //var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
                var numerovilla = await _numeroRepo.ObteneR(x => x.Id == id);
                if (numerovilla == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.IsExitoso = false;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<NumeroVillaDto>(numerovilla);
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
        public async Task<ActionResult<APIResponse>> PostNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _numeroRepo.ObteneR(v => v.Id == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("Nombre existe", "El numero villa con este nombre ya existe");
                    return BadRequest(ModelState);
                }

                if (await _villarepo.ObteneR(v => v.Id == createDto.VillaId) == null)
                {
                    ModelState.AddModelError("Clave foranea", "El Id de la villa no existe");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest();
                }

                NumeroVilla model = _mapper.Map<NumeroVilla>(createDto);

                await _numeroRepo.Crear(model);
                model.FechaDeCreacion= DateTime.Now;
                model.FechaDeActualizacion= DateTime.Now;
                _response.Resultado = model;
                _response.statusCode -= HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = model.Id }, _response);
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

                var numerovilla = await _numeroRepo.ObteneR(x => x.VillaId == id);
                if (numerovilla == null)
                {
                    _response.IsExitoso= false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _numeroRepo.Remover(numerovilla);
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
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] NumeroVillaUpdateDto UpdateDto)
        {
            if(UpdateDto == null || id!= UpdateDto.VillaId)
            {
                _response.IsExitoso= false;
                _response.statusCode= HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if(await _villarepo.ObteneR(x => x.Id == UpdateDto.VillaId)==null)
            {
                ModelState.AddModelError("Clave Foranea", "El id de la villa no existe");
                return BadRequest(ModelState);
            }

            NumeroVilla modelo= _mapper.Map<NumeroVilla>(UpdateDto);

            await _numeroRepo.Actualizar(modelo);
            _response.statusCode = HttpStatusCode.NoContent;


            return  Ok(_response);
        }
       
    }
}

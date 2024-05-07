using AutoMapper;
using MagicVilla_Utilidad;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.ViewModel;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class NumeroVillaController : Controller
    {
        private readonly INumeroVillaService _numeroVillaService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public NumeroVillaController(INumeroVillaService numeroVillaService, IMapper mapper, IVillaService villaService)
        {
            _numeroVillaService = numeroVillaService;
            _mapper = mapper;
            _villaService = villaService;
        }

        public async Task<IActionResult> IndexNumeroVilla ()
        {
            List<NumeroVillaDto> numeroVillaList = new();

            var response = await _numeroVillaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsExitoso)
            {
                numeroVillaList = JsonConvert.DeserializeObject<List<NumeroVillaDto>>(Convert.ToString(response.Resultado));

            }
            return View(numeroVillaList);
        }

        public async  Task<IActionResult> CrearNumeroVilla()
        {
            NumeroVillaViewModel numeroVM = new();

            var response = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsExitoso)
            {
                numeroVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)).
                    Select(v => new SelectListItem
                    {
                        Text = v.Nombre,
                        Value = v.Id.ToString()
                    });
            }
            return View(numeroVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearNumeroVilla(NumeroVillaViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.Crear<APIResponse>(modelo.NumeroVilla, HttpContext.Session.GetString(DS.SessionToken));
                if (response != null  && response.IsExitoso)
                {
                    TempData["exitoso"] = "NumeroVilla Creada Exitosamente";
                    return RedirectToAction(nameof(IndexNumeroVilla));
                }
                else
                {
                    if(response.ErrorMessages.Count>0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var res = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if (res != null && res.IsExitoso)
            {
                modelo.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Resultado)).
                    Select(v => new SelectListItem
                    {
                        Text = v.Nombre,
                        Value = v.Id.ToString()
                    });
            }

            return View(modelo);
        }

        public async Task<IActionResult> ActualizarNumeroVilla(int villaNo)
        {
            NumeroVillaUpdateViewModel numeroVillaM = new();

            var response = await _numeroVillaService.Obtener<APIResponse>(villaNo, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
                numeroVillaM.NumeroVilla = _mapper.Map<NumeroVillaUpdateDto>(modelo);


            }

            response = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsExitoso)
            {
                numeroVillaM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)).
                    Select(v => new SelectListItem
                    {
                        Text = v.Nombre,
                        Value = v.Id.ToString()
                    });

                return View(numeroVillaM);
            }

            return NotFound();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarNumeroVilla(NumeroVillaUpdateViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var response = await _numeroVillaService.Actualizar<APIResponse>(modelo.NumeroVilla, HttpContext.Session.GetString(DS.SessionToken));
                if (response != null && response.IsExitoso)
                {
                    TempData["exitoso"] = "NumeroVilla actualizada Exitosamente";
                    return RedirectToAction(nameof(IndexNumeroVilla));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var res = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if (res != null && res.IsExitoso)
            {
                modelo.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(res.Resultado)).
                    Select(v => new SelectListItem
                    {
                        Text = v.Nombre,
                        Value = v.Id.ToString()
                    });
            }

            return View(modelo);
        }

        public async Task<IActionResult> RemoverNumeroVilla(int villaNo)
        {
            NumeroVillaDeleteViewModel numeroVillaM = new();

            var response = await _numeroVillaService.Obtener<APIResponse>(villaNo, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                NumeroVillaDto modelo = JsonConvert.DeserializeObject<NumeroVillaDto>(Convert.ToString(response.Resultado));
                numeroVillaM.NumeroVilla = _mapper.Map<NumeroVillaDto>(modelo);


            }

            response = await _villaService.ObtenerTodos<APIResponse>(HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsExitoso)
            {
                numeroVillaM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Resultado)).
                    Select(v => new SelectListItem
                    {
                        Text = v.Nombre,
                        Value = v.Id.ToString()
                    });

                return View(numeroVillaM);
            }

            return NotFound();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverNumeroVilla(NumeroVillaDeleteViewModel modelo)
        {
            var response = await _numeroVillaService.Remover<APIResponse>(modelo.NumeroVilla.VillaNo, HttpContext.Session.GetString(DS.SessionToken));
            if (response != null && response.IsExitoso)
            {
                TempData["exitoso"] = "NumeroVilla Removida Exitosamente";
                return RedirectToAction(nameof(IndexNumeroVilla));
            }
            TempData["error"] = "Un error al remover";
            return View(modelo);
        }
    }
}

using MagicVilla_Utilidad;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _villaUrl;
        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration):base(httpClient)
        {
            _httpClient = httpClient;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
          
        }
        public Task<T> Actualizar<T>(VillaUpdateDto dto, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _villaUrl + "api/Villa/"+dto.Id,
                Token= Token
            });
        }

        public Task<T> Crear<T>(VillaCreateDto dto, string Token)
        {
           
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _villaUrl+"api/Villa",
                Token= Token
            });
        }

        public Task<T> Obtener<T>(int id, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "api/Villa/" + id,
                Token= Token
            });
        }

        public Task<T> ObtenerTodos<T>(string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "api/Villa",
                Token= Token
            });
        }

        public Task<T> Remover<T>(int id, string Token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _villaUrl + "api/Villa/" + id,
                Token= Token
            });
        }
    }
}

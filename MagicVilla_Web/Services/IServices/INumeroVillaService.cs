using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface INumeroVillaService
    {
        Task<T> ObtenerTodos<T>(string Token);
        Task<T> Obtener<T>(int id, string Token);
        Task<T> Crear<T>(NumeroVillaCreateDto dto, string Token);
        Task<T> Actualizar<T>(NumeroVillaUpdateDto dto, string Token);
        Task<T> Remover<T>(int id, string Token);
    }
}

using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> ObtenerTodos<T>(string Token);
        Task<T> Obtener<T>(int id, string Token);
        Task<T> Crear<T>(VillaCreateDto dto, string Token);
        Task<T> Actualizar<T>(VillaUpdateDto dto, string Token);
        Task<T> Remover<T>(int id, string Token);
    }
}

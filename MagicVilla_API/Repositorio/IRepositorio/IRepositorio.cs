using System.Linq.Expressions;

namespace MagicVilla_API.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {

        Task Crear(T entidad);

        Task<List<T>> Obtenertodos(Expression<Func<T, bool>>? filtro=null);

        Task<T> ObteneR(Expression<Func<T, bool>> filtro = null, bool tracked = true);

        Task Remover(T entidad);

        Task Grabar();
    }
}

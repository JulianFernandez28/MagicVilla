using MagicVilla_API.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext:IdentityDbContext<UsuarioAplicacion>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {

            
        }
        public DbSet<UsuarioAplicacion> UsuariosAplicacion { get; set; }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<NumeroVilla> NumeroVilla { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Nombre="Villa real",
                    Detalle="Detalle de la villa",
                    ImagenUrl="",
                    Ocupantes= 5,
                    MetrosCuadrados= 50,
                    Tarifa=200,
                    Amenidad="",
                    FechaCreacion= DateTime.Now,
                    FechaActualizacion= DateTime.Now,
                },
                 new Villa
                 {
                     Id = 2,
                     Nombre = "Premium vista a la piscina",
                     Detalle = "Detalle de la villa",
                     ImagenUrl = "",
                     Ocupantes = 4,
                     MetrosCuadrados = 40,
                     Tarifa = 150,
                     Amenidad = "",
                     FechaCreacion = DateTime.Now,
                     FechaActualizacion = DateTime.Now,
                 }
            );
        }
    }
}

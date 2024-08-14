using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Ua.Modelos;
using UAER.Modelos;

namespace UAER.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AreasS> AreasS { get; set; }
        public DbSet<Espacio> Espacios { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }

        public DbSet<SolicitarEspacio> SolicitarEspacios { get; set; }

        public DbSet<SolicitarMantenimiento> SolicitarMantenimientos { get; set; }
        public DbSet<UsuarioAplicacion> UsuarioAplicacion { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}

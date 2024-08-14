using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAER.Modelos;

namespace UAER.AccesoDatos.Configuracion
{
    public class SolicitarMantenimientoConfiguracion : IEntityTypeConfiguration<SolicitarMantenimiento>
    {
        public void Configure(EntityTypeBuilder<SolicitarMantenimiento> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NombreSolicitante).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Descripcion).HasMaxLength(1000);
            builder.Property(x => x.Estado).IsRequired();

            builder.Property(x => x.FechaSolicitud).IsRequired();
            builder.Property(x => x.FechaAsignadaFinal).IsRequired();
            builder.Property(x => x.FechaAsignadaInicio).IsRequired();


            builder.Property(x => x.AreasSId).IsRequired();
            builder.Property(x => x.MantenimientoId).IsRequired();

           






            builder.HasOne(x => x.AreasS).WithMany()
                .HasForeignKey(x => x.AreasSId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Mantenimiento).WithMany()
                .HasForeignKey(x => x.MantenimientoId)
                .OnDelete(DeleteBehavior.NoAction);

        }


    }
}

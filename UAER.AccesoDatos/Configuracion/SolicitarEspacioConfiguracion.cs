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
    public class SolicitarEspacioConfiguracion : IEntityTypeConfiguration<SolicitarEspacio>
    {
        public void Configure(EntityTypeBuilder<SolicitarEspacio> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NombreSolicitante).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Estado).IsRequired();

            builder.Property(x => x.FechaSolicitud).IsRequired();
            builder.Property(x => x.HoraSolicitud).IsRequired();


            builder.Property(x => x.AreasSId).IsRequired();
            builder.Property(x => x.EspacioId).IsRequired();

           

            builder.HasOne(x => x.AreasS).WithMany()
                .HasForeignKey(x => x.AreasSId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Espacio).WithMany()
                .HasForeignKey(x => x.EspacioId)
                .OnDelete(DeleteBehavior.NoAction);

        }


    }
}

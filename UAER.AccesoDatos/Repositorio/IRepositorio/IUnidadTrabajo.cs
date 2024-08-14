using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAER.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable
    {
        //Aqui se van  gregar todos los nuevos modelos
        IAreasSRepositorio AreasS { get; }
        IEspacioRepositorio Espacio { get; }
        IMantenimientoRepositorio Mantenimiento { get; }

        ISolicitarEspacioRepositorio SolicitarEspacio { get; }
        ISolicitarMantenimientoRepositorio SolicitarMantenimiento { get; }

        IUsuarioAplicacionRepositorio UsuarioAplicacion { get; }
        Task Guardar();

    }


}

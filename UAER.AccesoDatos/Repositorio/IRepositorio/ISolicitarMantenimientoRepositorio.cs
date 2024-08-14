using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAER.Modelos;

namespace UAER.AccesoDatos.Repositorio.IRepositorio
{
    public interface ISolicitarMantenimientoRepositorio : IRepositorio<SolicitarMantenimiento>
    {
        void Actualizar(SolicitarMantenimiento solicitarMantenimiento);
        IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj);
    }

}

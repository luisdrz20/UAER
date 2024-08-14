using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAER.AccesoDatos.Data;
using UAER.AccesoDatos.Repositorio.IRepositorio;
using UAER.Modelos;

namespace UAER.AccesoDatos.Repositorio
{
    public class SolicitarMantenimientoRepositorio : Repositorio<SolicitarMantenimiento>, ISolicitarMantenimientoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public SolicitarMantenimientoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(SolicitarMantenimiento solicitarMantenimiento)
        {
            var solicitarMantenimientoBD = _db.SolicitarMantenimientos.FirstOrDefault(b => b.Id == solicitarMantenimiento.Id);
            if (solicitarMantenimientoBD != null)
            {


                solicitarMantenimientoBD.AreasSId = solicitarMantenimiento.AreasSId;
                solicitarMantenimientoBD.NombreSolicitante = solicitarMantenimiento.NombreSolicitante;
                solicitarMantenimientoBD.MantenimientoId = solicitarMantenimiento.MantenimientoId;
                solicitarMantenimientoBD.FechaSolicitud = solicitarMantenimiento.FechaSolicitud;
                solicitarMantenimientoBD.Descripcion = solicitarMantenimiento.Descripcion;
                solicitarMantenimientoBD.FechaAsignadaInicio = solicitarMantenimiento.FechaAsignadaInicio;
                solicitarMantenimientoBD.FechaAsignadaFinal = solicitarMantenimiento.FechaAsignadaFinal;
                solicitarMantenimientoBD.Estado = solicitarMantenimiento.Estado;

                _db.SaveChanges();
            }
        }



        public IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj)
        {
            if (obj == "AreasS")
            {
                return _db.AreasS.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString(),
                });

            }

            if (obj == "Mantenimiento")
            {
                return _db.Mantenimientos.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString(),
                });

            }


            if (obj == "SolicitarMantenimiento")
            {
                return _db.SolicitarMantenimientos.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.NombreSolicitante,
                    Value = c.Id.ToString(),
                });

            }


            return null;
        }

    }


}

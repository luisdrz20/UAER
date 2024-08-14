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
    public class SolicitarEspacioRepositorio : Repositorio<SolicitarEspacio>, ISolicitarEspacioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public SolicitarEspacioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(SolicitarEspacio solicitarEspacio)
        {
            var solicitarEspacioBD = _db.SolicitarEspacios.FirstOrDefault(b => b.Id == solicitarEspacio.Id);
            if (solicitarEspacioBD != null)
            {


                solicitarEspacioBD.AreasSId = solicitarEspacio.AreasSId;
                solicitarEspacioBD.NombreSolicitante = solicitarEspacio.NombreSolicitante;
                solicitarEspacioBD.EspacioId = solicitarEspacio.EspacioId;
                solicitarEspacioBD.FechaSolicitud = solicitarEspacio.FechaSolicitud;
                solicitarEspacioBD.HoraSolicitud = solicitarEspacio.HoraSolicitud;
                solicitarEspacioBD.Estado = solicitarEspacio.Estado;

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

            if (obj == "Espacio")
            {
                return _db.Espacios.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString(),
                });

            }


            if (obj == "SolicitarEspacio")
            {
                return _db.SolicitarEspacios.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.NombreSolicitante,
                    Value = c.Id.ToString(),
                });

            }


            return null;
        }

    }


}

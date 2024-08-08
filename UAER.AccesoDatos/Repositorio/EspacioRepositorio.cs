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
    public class EspacioRepositorio : Repositorio<Espacio>, IEspacioRepositorio
    {
        private readonly ApplicationDbContext _db;

        public EspacioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Espacio espacio)
        {
            var espacioBD = _db.Espacios.FirstOrDefault(b => b.Id == espacio.Id);
            if (espacioBD != null)
            {
                espacioBD.Nombre = espacio.Nombre;
                espacioBD.Descripcion = espacio.Descripcion;
                espacioBD.Estado = espacio.Estado;
                _db.SaveChanges();
            }
        }
    }


}

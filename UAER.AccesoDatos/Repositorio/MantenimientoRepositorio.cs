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
    public class MantenimientoRepositorio : Repositorio<Mantenimiento>, IMantenimientoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MantenimientoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(Mantenimiento mantenimiento)
        {
            var mantenimientoBD = _db.Mantenimientos.FirstOrDefault(b => b.Id == mantenimiento.Id);
            if (mantenimientoBD != null)
            {
                mantenimientoBD.Nombre = mantenimiento.Nombre;
                mantenimientoBD.Descripcion = mantenimiento.Descripcion;
                mantenimientoBD.Estado = mantenimiento.Estado;
                _db.SaveChanges();
            }
        }
    }


}

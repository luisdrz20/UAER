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
    public class AreasSRepositorio : Repositorio<AreasS>, IAreasSRepositorio
    {
        private readonly ApplicationDbContext _db;

        public AreasSRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Actualizar(AreasS areasS)
        {
            var areasSBD = _db.AreasS.FirstOrDefault(b => b.Id == areasS.Id);
            if (areasSBD != null)
            {
                areasSBD.Nombre = areasS.Nombre;
                areasSBD.Descripcion = areasS.Descripcion;
                areasSBD.Estado = areasS.Estado;
                _db.SaveChanges();
            }
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAER.Modelos;

namespace UAER.AccesoDatos.Repositorio.IRepositorio
{
    public interface IEspacioRepositorio : IRepositorio<Espacio>
    {
        void Actualizar(Espacio espacio);
    }

}

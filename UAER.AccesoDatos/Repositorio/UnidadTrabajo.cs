﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAER.AccesoDatos.Data;
using UAER.AccesoDatos.Repositorio.IRepositorio;
using UAER.Modelos;

namespace UAER.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IAreasSRepositorio AreasS { get; set; }
        public IEspacioRepositorio Espacio { get; set; }
        public IMantenimientoRepositorio Mantenimiento { get; set; }

        public ISolicitarEspacioRepositorio SolicitarEspacio { get; set; }

        public ISolicitarMantenimientoRepositorio SolicitarMantenimiento { get; set; }

        public IUsuarioAplicacionRepositorio UsuarioAplicacion { get; set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            AreasS = new AreasSRepositorio(_db);
            Espacio = new EspacioRepositorio(_db);
            Mantenimiento = new MantenimientoRepositorio(_db);
            SolicitarEspacio = new SolicitarEspacioRepositorio(_db);
            SolicitarMantenimiento = new SolicitarMantenimientoRepositorio(_db);
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }

}

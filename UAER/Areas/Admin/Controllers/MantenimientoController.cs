using Microsoft.AspNetCore.Mvc;
using UAER.AccesoDatos.Repositorio.IRepositorio;
using UAER.Modelos;
using UAER.Utilidades;

namespace UAER.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MantenimientoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public MantenimientoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Metodo Upsert GET
        public async Task<IActionResult> Upsert(int? id)
        {
            Mantenimiento mantenimiento = new Mantenimiento();
            if (id == null)
            {
                //Creamos un nuevo registro
                mantenimiento.Estado = true;
                return View(mantenimiento);
            }
            mantenimiento = await _unidadTrabajo.Mantenimiento.Obtener(id.GetValueOrDefault());
            if (mantenimiento == null)
            {
                return NotFound();
            }
            return View(mantenimiento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                if (mantenimiento.Id == 0)
                {
                    await _unidadTrabajo.Mantenimiento.Agregar(mantenimiento);
                    TempData[DS.Exitosa] = "El mantenimiento se creo con exito";
                }
                else
                {
                    _unidadTrabajo.Mantenimiento.Actualizar(mantenimiento);
                    TempData[DS.Exitosa] = "El mantenimiento se actualizo con exito";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar el mantenimiento";
            return View(mantenimiento);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var mantenimientoDB = await _unidadTrabajo.Mantenimiento.Obtener(id);
            if (mantenimientoDB == null)
            {
                return Json(new { success = false, message = "Error al borrar el registro en la Base de datos" });
            }
            _unidadTrabajo.Mantenimiento.Remover(mantenimientoDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Mantenimiento eliminado con exito" });
        }





        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Mantenimiento.ObtenerTodos();
            return Json(new { data = todos });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Mantenimiento.ObtenerTodos();

            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim()
                                    == nombre.ToLower().Trim()
                                    && b.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }





        #endregion
    }

}

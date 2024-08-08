using Microsoft.AspNetCore.Mvc;
using UAER.AccesoDatos.Repositorio.IRepositorio;
using UAER.Modelos;
using UAER.Utilidades;

namespace UAER.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EspacioController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public EspacioController(IUnidadTrabajo unidadTrabajo)
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
            Espacio espacio = new Espacio();
            if (id == null)
            {
                //Creamos un nuevo registro
                espacio.Estado = true;
                return View(espacio);
            }
            espacio = await _unidadTrabajo.Espacio.Obtener(id.GetValueOrDefault());
            if (espacio == null)
            {
                return NotFound();
            }
            return View(espacio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Espacio espacio)
        {
            if (ModelState.IsValid)
            {
                if (espacio.Id == 0)
                {
                    await _unidadTrabajo.Espacio.Agregar(espacio);
                    TempData[DS.Exitosa] = "El espacio se creo con exito";
                }
                else
                {
                    _unidadTrabajo.Espacio.Actualizar(espacio);
                    TempData[DS.Exitosa] = "El espacio se actualizo con exito";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar el espacio";
            return View(espacio);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var espacioDB = await _unidadTrabajo.Espacio.Obtener(id);
            if (espacioDB == null)
            {
                return Json(new { success = false, message = "Error al borrar el registro en la Base de datos" });
            }
            _unidadTrabajo.Espacio.Remover(espacioDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Espacio eliminado con exito" });
        }





        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Espacio.ObtenerTodos();
            return Json(new { data = todos });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Espacio.ObtenerTodos();

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

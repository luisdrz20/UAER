using Microsoft.AspNetCore.Mvc;
using UAER.AccesoDatos.Repositorio.IRepositorio;
using UAER.Modelos;
using UAER.Utilidades;

namespace UAER.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AreasSController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public AreasSController(IUnidadTrabajo unidadTrabajo)
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
            AreasS areasS = new AreasS();
            if (id == null)
            {
                //Creamos un nuevo registro
                areasS.Estado = true;
                return View(areasS);
            }
            areasS = await _unidadTrabajo.AreasS.Obtener(id.GetValueOrDefault());
            if (areasS == null)
            {
                return NotFound();
            }
            return View(areasS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AreasS areasS)
        {
            if (ModelState.IsValid)
            {
                if (areasS.Id == 0)
                {
                    await _unidadTrabajo.AreasS.Agregar(areasS);
                    TempData[DS.Exitosa] = "La area se creo con exito";
                }
                else
                {
                    _unidadTrabajo.AreasS.Actualizar(areasS);
                    TempData[DS.Exitosa] = "La area se actualizo con exito";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar la area";
            return View(areasS);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var areasSDB = await _unidadTrabajo.AreasS.Obtener(id);
            if (areasSDB == null)
            {
                return Json(new { success = false, message = "Error al borrar el rgistro en la Base de datos" });
            }
            _unidadTrabajo.AreasS.Remover(areasSDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Area eliminada con exito" });
        }





        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.AreasS.ObtenerTodos();
            return Json(new { data = todos });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.AreasS.ObtenerTodos();

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

using Microsoft.AspNetCore.Mvc;
using UAER.AccesoDatos.Repositorio.IRepositorio;
using UAER.Modelos;
using UAER.Modelos.ViewModels;
using UAER.Utilidades;

namespace UAER.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SolicitarMantenimientoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public SolicitarMantenimientoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }
        //Metodo Upsert GET
        public async Task<IActionResult> Upsert(int? id)
        {
            SolicitarMantenimientoVM solicitarMantenimientoVM = new SolicitarMantenimientoVM()
            {
                SolicitarMantenimiento = new SolicitarMantenimiento(),
                AreasSLista = _unidadTrabajo.SolicitarMantenimiento.ObtenerTodosDropDownList("AreasS"),
                MantenimientoLista = _unidadTrabajo.SolicitarMantenimiento.ObtenerTodosDropDownList("Mantenimiento")
            };
            if (id == null)
            {
                //Crear un  SolicitarMantenimientoVM
                return View(solicitarMantenimientoVM);

            }
            else
            {
                //Actualizar una solicitud de Mantenimiento existente
                solicitarMantenimientoVM.SolicitarMantenimiento = await _unidadTrabajo.SolicitarMantenimiento
                    .Obtener(id.GetValueOrDefault());
                if (solicitarMantenimientoVM.SolicitarMantenimiento == null)
                {
                    return NotFound();
                }
                return View(solicitarMantenimientoVM);
            }
        }

        #region API

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SolicitarMantenimientoVM mantenimientoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (mantenimientoVM.SolicitarMantenimiento.Id == 0)
                {
                    ////crear un nuevo producto
                    //string upload = webRootPath + DS.ImagenRuta;
                    ////Crear un id unico en mi sistema
                    //string fileName = Guid.NewGuid().ToString();
                    ////creamos una variable para conocer la extencion del archivo
                    //string extension = Path.GetExtension(files[0].FileName);

                    ////habilitar el filestream para crear el archivo de imagen en tiempo real 
                    //using (var filestream = new FileStream(Path.Combine(upload, fileName + extension)
                    //                                       , FileMode.Create))
                    //{
                    //    files[0].CopyTo(filestream);
                    //}

                    //productoVM.SolicitarMantenimiento.ImagenUrl = fileName + extension;
                    await _unidadTrabajo.SolicitarMantenimiento.Agregar(mantenimientoVM.SolicitarMantenimiento);
                }
                else
                {
                    //Actualizar al producto
                    var objProducto = await _unidadTrabajo.SolicitarMantenimiento
                                                .ObtenerPrimero(p => p.Id == mantenimientoVM.SolicitarMantenimiento.Id
                                                , isTracking: false);
                    //if (files.Count > 0)
                    //{
                    //    //string upload = webRootPath + DS.ImagenRuta;
                    //    //string fileName = Guid.NewGuid().ToString();
                    //    //string extension = Path.GetExtension(files[0].FileName);

                    //    ////borrar la imagen anterior 
                    //    //var anteriorFile = Path.Combine(upload, objProducto.ImagenUrl);

                    //    ////verificamos que la imagen exista

                    //    //if (System.IO.File.Exists(anteriorFile))
                    //    //{
                    //    //    System.IO.File.Delete(anteriorFile);
                    //    //}

                    //    ////creamos la nueva imagen
                    //    //using (var filestream = new FileStream(
                    //    //        Path.Combine(upload, fileName + extension)
                    //    //        , FileMode.Create))
                    //    //{
                    //    //    files[0].CopyTo(filestream);
                    //    //}

                    //    //productoVM.SolicitarMantenimiento.ImagenUrl = fileName + extension;

                    //}// si no elige imagen 
                    //else
                    //{
                    //    //productoVM.SolicitarMantenimiento.ImagenUrl = objProducto.ImagenUrl;
                    //}
                    _unidadTrabajo.SolicitarMantenimiento.Actualizar(mantenimientoVM.SolicitarMantenimiento);
                }
                TempData[DS.Exitosa] = "Producto Registrado";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }
            mantenimientoVM.AreasSLista = _unidadTrabajo.SolicitarMantenimiento.ObtenerTodosDropDownList("AreasS");
            mantenimientoVM.MantenimientoLista = _unidadTrabajo.SolicitarMantenimiento.ObtenerTodosDropDownList("Mantenimiento");
     
            return View(mantenimientoVM);
        }







        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var solicitarMantenimientoDB = await _unidadTrabajo.SolicitarMantenimiento.Obtener(id);
            if (solicitarMantenimientoDB == null)
            {
                return Json(new { success = false, message = "Error al borrar el reistro en la Base de datos" });
            }
            _unidadTrabajo.SolicitarMantenimiento.Remover(solicitarMantenimientoDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Solicitud de Mantenimiento eliminado con exito" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.SolicitarMantenimiento.ObtenerTodos(incluirPropiedades: "AreasS,Mantenimiento");
            return Json(new { data = todos });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string serie, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.SolicitarMantenimiento.ObtenerTodos();

            if (id == 0)
            {
                valor = lista.Any(b => b.NombreSolicitante.ToLower().Trim() == serie.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NombreSolicitante.ToLower().Trim()
                                    == serie.ToLower().Trim()
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

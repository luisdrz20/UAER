using Microsoft.AspNetCore.Mvc;
using UAER.AccesoDatos.Repositorio.IRepositorio;
using UAER.Modelos;
using UAER.Modelos.ViewModels;
using UAER.Utilidades;

namespace UAER.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SolicitarEspacioController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
       

        public SolicitarEspacioController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
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
            SolicitarEspacioVM solicitarEspacioVM = new SolicitarEspacioVM()
            {
                SolicitarEspacio = new SolicitarEspacio(),
                AreasSLista = _unidadTrabajo.SolicitarEspacio.ObtenerTodosDropDownList("AreasS"),
                EspacioLista = _unidadTrabajo.SolicitarEspacio.ObtenerTodosDropDownList("Espacio")
            };
            if (id == null)
            {
                //Crear un  SolicitarEspacioVM
                return View(solicitarEspacioVM);

            }
            else
            {
                //Actualizar una solicitud de espacio existente
                solicitarEspacioVM.SolicitarEspacio = await _unidadTrabajo.SolicitarEspacio
                    .Obtener(id.GetValueOrDefault());
                if (solicitarEspacioVM.SolicitarEspacio == null)
                {
                    return NotFound();
                }
                return View(solicitarEspacioVM);
            }
        }

        #region API

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SolicitarEspacioVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productoVM.SolicitarEspacio.Id == 0)
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

                    //productoVM.SolicitarEspacio.ImagenUrl = fileName + extension;
                    await _unidadTrabajo.SolicitarEspacio.Agregar(productoVM.SolicitarEspacio);
                }
                else
                {
                    //Actualizar al producto
                    var objProducto = await _unidadTrabajo.SolicitarEspacio
                                                .ObtenerPrimero(p => p.Id == productoVM.SolicitarEspacio.Id
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

                    //    //productoVM.SolicitarEspacio.ImagenUrl = fileName + extension;

                    //}// si no elige imagen 
                    //else
                    //{
                    //    //productoVM.SolicitarEspacio.ImagenUrl = objProducto.ImagenUrl;
                    //}
                    _unidadTrabajo.SolicitarEspacio.Actualizar(productoVM.SolicitarEspacio);
                }
                TempData[DS.Exitosa] = "Producto Registrado";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }
            productoVM.AreasSLista = _unidadTrabajo.SolicitarEspacio.ObtenerTodosDropDownList("AreasS");
            productoVM.EspacioLista = _unidadTrabajo.SolicitarEspacio.ObtenerTodosDropDownList("Espacio");
     
            return View(productoVM);
        }







        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var solicitarEspacioDB = await _unidadTrabajo.SolicitarEspacio.Obtener(id);
            if (solicitarEspacioDB == null)
            {
                return Json(new { success = false, message = "Error al borrar el reistro en la Base de datos" });
            }
            _unidadTrabajo.SolicitarEspacio.Remover(solicitarEspacioDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Solicitud de espacio eliminado con exito" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.SolicitarEspacio.ObtenerTodos(incluirPropiedades: "AreasS,Espacio");
            return Json(new { data = todos });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string serie, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.SolicitarEspacio.ObtenerTodos();

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

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAER.Modelos
{
    public class UsuarioAplicacion :IdentityUser
    {

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength (80, ErrorMessage ="Maximo 80 caracteres")]
        public string Nombres { get; set; }


        [Required(ErrorMessage = "Los apellidos es requerido")]
        [MaxLength(80, ErrorMessage = "Maximo 80 caracteres")]
        public  string Apellidos { get; set; }


        [Required(ErrorMessage = "El telefono es requerida")]
        [MaxLength(200, ErrorMessage = "Maximo 200 caracteres")]
        public string Telefono { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UAER.Modelos
{
    public class SolicitarEspacio
    {
        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "Nombre del solicitante es requerido")]
        [MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string NombreSolicitante { get; set; }


        [Required(ErrorMessage = "El estado es requerido")]
        public bool Estado { get; set; }

        // Fecha de la solicitud
        [Required(ErrorMessage = "La fecha es requerida")]
        [Column(TypeName = "date")] // Indica a EF que debe mapearse como tipo DATE en la base de datos
        public DateTime FechaSolicitud { get; set; }

        // Hora de la solicitud
        [Required(ErrorMessage = "La hora es requerida")]
        public TimeSpan HoraSolicitud { get; set; }






        //raleciones
        [Required(ErrorMessage = "El area del solicitante es requerida")]
        public int AreasSId { get; set; }
        [ForeignKey("AreasSId")]
        public AreasS AreasS { get; set; }


      
        [Required(ErrorMessage = "El Espacio es requerido")]
        public int EspacioId { get; set; }
        [ForeignKey("EspacioId")]
        public Espacio Espacio { get; set; }


    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAER.Modelos.ViewModels
{
    public class SolicitarEspacioVM
    {
        public SolicitarEspacio SolicitarEspacio { get; set; }
        public IEnumerable<SelectListItem> AreasSLista { get; set; }
        public IEnumerable<SelectListItem> EspacioLista { get; set; }

    }
}

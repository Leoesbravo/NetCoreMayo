using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Grupo
    {
        public int? IdGrupo { get; set; }
        public string? Horario { get; set; }
        public string Nombre { get; set; }
        public ML.Materia Materia { get; set; }
        public List<object>? Grupos { get; set; }
        public ML.Plantel Plantel { get; set; }
    }
}

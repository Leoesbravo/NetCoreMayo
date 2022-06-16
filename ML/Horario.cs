using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Horario
    {
        public int IdHorario { get; set; }
        public string Turno { get; set; }
        public ML.Materia Materia { get; set; }
        public ML.Grupo Grupo  { get; set; }
    }
}

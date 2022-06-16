using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Semestre
    {
        public Semestre()
        {
            Materia = new HashSet<Materium>();
        }

        public int IdSemestre { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Materium> Materia { get; set; }
    }
}

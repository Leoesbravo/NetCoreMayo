using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Plantel
    {
        public Plantel()
        {
            Grupos = new HashSet<Grupo>();
        }

        public int IdPlantel { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Grupo> Grupos { get; set; }
    }
}

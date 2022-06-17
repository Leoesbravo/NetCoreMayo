using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Grupo
    {
        public Grupo()
        {
            Horarios = new HashSet<Horario>();
        }

        public int IdGrupo { get; set; }
        public string? Nombre { get; set; }
        public int? IdPlantel { get; set; }

        public virtual Plantel? IdPlantelNavigation { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }
    }
}

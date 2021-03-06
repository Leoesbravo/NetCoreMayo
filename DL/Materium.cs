using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Materium
    {
        public Materium()
        {
            Horarios = new HashSet<Horario>();
        }

        public int IdMateria { get; set; }
        public string? Nombre { get; set; }
        public decimal? Costo { get; set; }
        public byte? Creditos { get; set; }
        public string? Descripcion { get; set; }
        public int? IdSemestre { get; set; }
        public string? Imagen { get; set; }
        public bool? Estatus { get; set; }

        public virtual Semestre? IdSemestreNavigation { get; set; }
        public virtual ICollection<Horario> Horarios { get; set; }
    }
}

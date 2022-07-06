using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Materia
    {
        public int? IdMateria { get; set; }

        [Required(ErrorMessage = "Debes de insertar nombre")]
        public string Nombre { get; set; }

        [Required]
        public byte Creditos { get; set; }

        [Required(ErrorMessage = "Debes de insertar nombre")]
        public decimal Costo { get; set; }
        public string Descripcion { get; set; }

        public ML.Semestre Semestre { get; set; }

        public List<object>? Materias { get; set; }
        public ML.Plantel? Plantel { get; set; }
        public ML.Grupo? Grupo { get; set; }
        public string? Imagen { get; set; }
        public ML.Horario? Horario { get; set; }
        public bool Estatus { get; set; }
    }
}
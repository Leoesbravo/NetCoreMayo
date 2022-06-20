namespace ML
{
    public class Materia
    {
        public int? IdMateria { get; set; }
        public string Nombre { get; set; }
        public byte Creditos { get; set; }
        public decimal Costo { get; set; }
        public string Descripcion { get; set; }
        public ML.Semestre Semestre { get; set; }
        public List<object> Materias { get; set; }
        public ML.Plantel Plantel { get; set; }
        public ML.Grupo Grupo { get; set; }
        public string? Imagen { get; set; }
        public ML.Horario Horario { get; set; }
        public bool Estatus { get; set; }
    }
}
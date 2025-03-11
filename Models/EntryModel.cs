namespace QuickType
{

    public partial class Actividades
    {
        public Tareas Tareas { get; set; }
    }

    public partial class Tareas
    {
        public string Nombre { get; set; }
        public DateTimeOffset? Fecha_creacion { get; set; }
        public string Estado { get; set; }
        public List<Responsable> Responsables { get; set; }
    }

    public partial class Responsable
    {
        public string Nombre { get; set; }
    }
}

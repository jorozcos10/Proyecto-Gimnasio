namespace OlimpoBlazor.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Cedula { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; } // Administrador, Entrenador, Cliente
    }
}

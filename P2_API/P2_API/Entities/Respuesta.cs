namespace P2_API.Entities
{
    public class Respuesta
    {
        public int Codigo { get; set; }
        public string? Mensaje { get; set; } = "OK";
        public object? Contenido { get; set; }

    }
}

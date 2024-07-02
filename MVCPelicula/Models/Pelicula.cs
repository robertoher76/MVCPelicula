namespace MVCPelicula.Models
{
    public class Pelicula
    {
        public int ID { get; set; }
        public string Titutlo { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string Genero { get; set; }
        public decimal Precio { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCPelicula.Models
{
    public class Pelicula
    {
        public int ID { get; set; }

        [StringLength(250)]
        [Required]
        public string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; }

        //propiedad para la llave foranea        
        [Required]
        [ForeignKey("Genero")]
        public int? GeneroId { get; set; }

        //propiedad de navegacion
        public Genero Genero { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [StringLength(250)]
        [Required]
        public string Director { get; set; }
    }
}

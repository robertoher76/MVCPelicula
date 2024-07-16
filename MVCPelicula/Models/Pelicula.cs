﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCPelicula.Models
{
    public class Pelicula
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Título")]
        [Required(ErrorMessage = "El campo título es requerido")]
        public string Titulo { get; set; }

        [Display(Name = "Fecha de Lanzamiento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo fecha de lanzamiento es requerido")]
        public DateTime FechaLanzamiento { get; set; }

        //propiedad para la llave foranea        
        [Required]
        [ForeignKey("Genero")]
        [Display(Name = "Género")]
        public int? GeneroId { get; set; }

        //propiedad de navegacion
        public Genero Genero { get; set; }

        [Range(1, 100)]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "El campo director es requerido")]
        public string Director { get; set; }
    }
}

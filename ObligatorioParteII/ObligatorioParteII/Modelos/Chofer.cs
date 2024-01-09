using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ObligatorioParteII.Modelos{

    public class Chofer{
      

        [Range(8, int.MaxValue, ErrorMessage = "Debe tener 8 dígitos, sin puntos ni guiónes.")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CI { get; set; }
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [Range(18, int.MaxValue, ErrorMessage = "Debe ser mayor de edad.")]
        public int Edad {  get; set; }
        public string Vehiculo { get; set; }

        public bool Modificado { get; set; }


        public Chofer()
        {
        }
    }
}

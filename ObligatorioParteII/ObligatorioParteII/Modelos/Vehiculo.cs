using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ObligatorioParteII.Modelos{
    public class Vehiculo{

        [Range(5, int.MaxValue, ErrorMessage ="Debe ingresar 5 dígitos.")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CN { get; set; }
        [RegularExpression(@"^[A-Za-z]{3} \d{4}$", ErrorMessage = "Debe tener tres letras, un espacio y cuatro números.")]
        public string Matricula { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        [Range(8, int.MaxValue, ErrorMessage = "El año debe contener 4 dígitos.")]
        public int Anio { get; set; }


        public Vehiculo(){
            CN = string.Empty;
            Matricula = string.Empty;
            Marca = string.Empty;
            Modelo = string.Empty;
        }
        public class ApplicationDbContext : DbContext{
            public DbSet<Vehiculo> Vehiculos { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder){
                modelBuilder.Entity<Vehiculo>()
                    .HasIndex(v => v.Matricula)
                    .IsUnique();
            }
        }
    }
}

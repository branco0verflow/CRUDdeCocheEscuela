using Microsoft.EntityFrameworkCore;
using ObligatorioParteII.Modelos;

namespace ObligatorioParteII.Datos
{

    // Clase para interactuar con la base de datos
    public class ApplicationDbContextt: DbContext
    {
        //Atributos de la clase
        public DbSet<Vehiculo> Vehiculos { get; set; }

        public DbSet<Chofer> Choferes { get; set; }

        // Métodos internos para obtener vehiculos con mayor antiguedad
        internal async Task<Vehiculo> ObtenerMasAntiguoAsync()
        {
            return await Vehiculos.OrderBy(c => c.Anio).FirstOrDefaultAsync();
        }

        // Constructor 

        public ApplicationDbContextt(DbContextOptions<ApplicationDbContextt> options)
         : base(options)
        {
            Vehiculos = Set<Vehiculo>();
        }
    }
}

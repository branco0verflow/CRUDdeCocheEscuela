using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParteII.Datos;
using ObligatorioParteII.Modelos;

namespace ObligatorioParteII.Pages.VehiculosPag{
    public class vehiculosDisponiblesModel : PageModel{

        private readonly ApplicationDbContextt _contexto;
        public IEnumerable<Vehiculo> Vehiculos { get; set; }


        public vehiculosDisponiblesModel(ApplicationDbContextt contexto){
            _contexto = contexto;
        }
        public async Task OnGet(){

            List<string> matriculas = await _contexto.Choferes
              .Where(chofer => chofer.Vehiculo != "Sin asignar")
              .Select(chofer => chofer.Vehiculo)
              .ToListAsync();
            List<Vehiculo> vehiculosSinMatriculasEspecificas = await _contexto.Vehiculos
            .Where(vehiculo => !matriculas.Contains(vehiculo.Matricula))
             .ToListAsync();

            Vehiculos = vehiculosSinMatriculasEspecificas;
        }
    }
}

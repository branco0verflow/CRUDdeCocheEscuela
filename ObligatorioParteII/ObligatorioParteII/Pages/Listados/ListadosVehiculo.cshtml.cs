using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObligatorioParteII.Datos;
using ObligatorioParteII.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ObligatorioParteII.Pages.Listados{
    public class ListadosVehiculoModel : PageModel{


        private readonly ApplicationDbContextt _contexto;
        public IEnumerable<Vehiculo> Vehiculos { get; set; }


        public ListadosVehiculoModel(ApplicationDbContextt contexto){
            _contexto = contexto;
        }
        public async Task OnGet(){
            Vehiculos = await _contexto.Vehiculos.ToListAsync();
        }
    }
}

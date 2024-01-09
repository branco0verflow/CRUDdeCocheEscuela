using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParteII.Datos;
using ObligatorioParteII.Modelos;

namespace ObligatorioParteII.Pages.VehiculosPag{
    public class VehiculosModel : PageModel{


        private readonly ApplicationDbContextt _contexto;
        public IEnumerable<Vehiculo> Vehiculos { get; set; }
        [TempData]
        public string Mensaje { get; set; }
        public Notificacion Notificacion { get; set; }


        public VehiculosModel(ApplicationDbContextt contexto){
            _contexto = contexto;
            Notificacion = Notificacion.getInstance();
        }
        public async Task OnGet(){
            Vehiculos = await _contexto.Vehiculos.ToListAsync();
        }
        public async Task<IActionResult> OnPostBorrar(String cn){

            var vehiculo = await _contexto.Vehiculos.FindAsync(cn);
            Notificacion.Estado = true;

            if (vehiculo == null){
                return NotFound();
            }

            _contexto.Vehiculos.Remove(vehiculo);
            await _contexto.SaveChangesAsync();
            Mensaje = "Vehículo eliminado correctamente.";
            Notificacion.Mensaje = Mensaje;
            return RedirectToPage();
        }
    }
}

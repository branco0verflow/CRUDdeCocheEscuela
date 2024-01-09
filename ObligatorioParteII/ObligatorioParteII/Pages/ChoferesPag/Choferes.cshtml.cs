using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParteII.Datos;
using ObligatorioParteII.Modelos;

namespace ObligatorioParteII.Pages.ChoferesPag
{
    public class ChoferesModel : PageModel{


        private readonly ApplicationDbContextt _contexto;
        public IEnumerable<Chofer> Choferes { get; set; }
        [TempData]
        public string Mensaje { get; set; }
        public Notificacion Notificacion { get; set; }


        public ChoferesModel(ApplicationDbContextt contexto){
            _contexto = contexto;
            Notificacion = Notificacion.getInstance();
        }
        public async Task OnGet(){
            Choferes = await _contexto.Choferes.ToListAsync();
        }
        public async Task<IActionResult> OnPostBorrar(int ci){

            var chofer = await _contexto.Choferes.FindAsync(ci);
            Notificacion.Estado = true;

            if (chofer == null){
                return NotFound();
            }

            if(chofer.Vehiculo == "Sin asignar"){

                _contexto.Choferes.Remove(chofer);
                await _contexto.SaveChangesAsync();
                Mensaje = "Chofer eliminado correctamente.";
                Notificacion.Mensaje = Mensaje;
                
            }else{
                Mensaje = "No se puede eliminar, tiene un vehículo asignado.";
                Notificacion.Mensaje = Mensaje;
            }
            return RedirectToPage();
        }
    }
}

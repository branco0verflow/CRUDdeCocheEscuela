using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParteII.Datos;
using ObligatorioParteII.Modelos;

namespace ObligatorioParteII.Pages.VehiculosPag{
    public class CrearModel : PageModel{


        private readonly ApplicationDbContextt _contexto;
        [BindProperty]
        public Vehiculo Vehiculos { get; set; }
        [TempData]
        public string Mensaje { get; set; }
        public Notificacion Notificacion { get; set; }


        public CrearModel(ApplicationDbContextt contexto){
            _contexto = contexto;
            Notificacion = Notificacion.getInstance();
        }
        public async Task<IActionResult> OnPost(){

            if (!ModelState.IsValid){
                return Page();
            }
            
            Vehiculo veiculoMatriculaExistente = await _contexto.Vehiculos.FirstOrDefaultAsync(v => v.Matricula == Vehiculos.Matricula);
            Vehiculo veiculoCNExistente = await _contexto.Vehiculos.FirstOrDefaultAsync(v => v.CN == Vehiculos.CN);

            if (veiculoMatriculaExistente == null && veiculoCNExistente == null){
                _contexto.Add(Vehiculos);
                await _contexto.SaveChangesAsync();
                Notificacion.Estado = true;
                Mensaje = "Vehículo agregado correctamente";
                Notificacion.Mensaje = Mensaje;
                return RedirectToPage("Vehiculos");
            }

            if(veiculoMatriculaExistente != null){
                ModelState.AddModelError("Vehiculos.Matricula", $"Ya existe un vehículo con la matrícula: {Vehiculos.Matricula}");
            }

            if (veiculoCNExistente != null){
                ModelState.AddModelError("Vehiculos.CN", $"Ya existe un vehículo con el CN: {Vehiculos.CN}");
            }
        
            return Page();
        }
    }
}

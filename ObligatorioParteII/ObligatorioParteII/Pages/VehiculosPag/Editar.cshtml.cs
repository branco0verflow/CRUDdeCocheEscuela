using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioParteII.Datos;
using ObligatorioParteII.Modelos;

namespace ObligatorioParteII.Pages.VehiculosPag{
    public class EditarModel : PageModel{


        private readonly ApplicationDbContextt _contexto;
        [BindProperty]
        public Vehiculo Vehiculo { get; set; }
        [TempData]
        public string Mensaje { get; set; }
        public Notificacion Notificacion { get; set;}


        public EditarModel(ApplicationDbContextt contexto){
            _contexto = contexto;
            Notificacion = Notificacion.getInstance();
        }
        public async Task OnGet(string cn){
            Vehiculo = await _contexto.Vehiculos.FindAsync(cn);
        }

        public async Task<IActionResult> OnPost(){

            if (ModelState.IsValid){

                var VehiculoDesdeDB = await
               _contexto.Vehiculos.FindAsync(Vehiculo.CN);
                Vehiculo veiculoMatriculaExistente = await _contexto.Vehiculos.FirstOrDefaultAsync(v => v.Matricula == Vehiculo.Matricula);

                if (veiculoMatriculaExistente != null && veiculoMatriculaExistente.Matricula != VehiculoDesdeDB.Matricula){
                    ModelState.AddModelError("Vehiculo.Matricula", $"Ya existe un vehículo con la matrícula: {Vehiculo.Matricula}");
                    return Page();
                }else if (VehiculoDesdeDB != null){
                    VehiculoDesdeDB.CN = Vehiculo.CN;
                    VehiculoDesdeDB.Matricula = Vehiculo.Matricula;
                    VehiculoDesdeDB.Marca = Vehiculo.Marca;
                    VehiculoDesdeDB.Modelo = Vehiculo.Modelo;
                    VehiculoDesdeDB.Anio = Vehiculo.Anio;
                    await _contexto.SaveChangesAsync();
                    Notificacion.Estado = true;
                    Mensaje = "Datos modificados con éxito.";
                    Notificacion.Mensaje = Mensaje;
                    return RedirectToPage("Vehiculos");
                }else{
                    return NotFound();
                }
            }
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObligatorioParteII.Datos;
using ObligatorioParteII.Modelos;
using Microsoft.EntityFrameworkCore;


namespace ObligatorioParteII.Pages.ChoferesPag
{
    public class CrearModel : PageModel { 


        private readonly ApplicationDbContextt _contexto;
        [BindProperty]
        public Chofer Chofer { get; set; }
        [TempData]
        public string Mensaje { get; set; }
        public IEnumerable<Vehiculo> Vehiculos { get; set; }
        public Notificacion Notificacion { get; set; }



        public CrearModel(ApplicationDbContextt contexto){
            _contexto = contexto;
            Notificacion = Notificacion.getInstance();
        }
        public async Task OnGet(){
            Vehiculos = await _contexto.Vehiculos.ToListAsync();
        }
        public async Task<IActionResult> OnPost(){

            if (!ModelState.IsValid){
                return Page();
            }

            Chofer choferExistente = await _contexto.Choferes.FirstOrDefaultAsync(chofer => chofer.CI == Chofer.CI);
            Chofer choferConMatriculaIgual = await _contexto.Choferes
             .Where(chofer => chofer.Vehiculo == Chofer.Vehiculo && chofer.Vehiculo != "Sin asignar")
             .FirstOrDefaultAsync();

            if (choferExistente == null && choferConMatriculaIgual==null ){

                _contexto.Add(Chofer);
                await _contexto.SaveChangesAsync();
                Notificacion.Estado = true;
                Mensaje = "El nuevo chofer se creó correctamente";
                Notificacion.Mensaje = Mensaje;
                return RedirectToPage("Choferes");
            }

            if(choferExistente != null){
                ModelState.AddModelError("Chofer.CI", $"Ya existe un usuario con CI: {Chofer.CI}.");
            }

            if (choferConMatriculaIgual != null){
                ModelState.AddModelError("Chofer.Vehiculo", $"Ya existe un usuario con la matricula: {Chofer.Vehiculo} asignada.");
            }

            Vehiculos = await _contexto.Vehiculos.ToListAsync();
            return Page();
        }
    }
}

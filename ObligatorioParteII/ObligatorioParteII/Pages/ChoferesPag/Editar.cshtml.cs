using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObligatorioParteII.Datos;
using ObligatorioParteII.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ObligatorioParteII.Pages.ChoferesPag{
    public class EditarModel : PageModel{

        private readonly ApplicationDbContextt _contexto;
        [BindProperty]
        public Chofer Chofer { get; set; }
        [TempData]
        public string Mensaje { get; set; }
        public Notificacion Notificacion { get; set; }
        public IEnumerable<Vehiculo> Vehiculos { get; set; }


        public EditarModel(ApplicationDbContextt contexto)
        {
            _contexto = contexto;
            Notificacion = Notificacion.getInstance();
        }
       
        public async Task OnGet(int ci)
        {
            Chofer = await _contexto.Choferes.FindAsync(ci);
            Vehiculos = await _contexto.Vehiculos.ToListAsync();
        }



        public async Task<IActionResult> OnPost(){
            
            if (ModelState.IsValid){

                var ChoferDesdeDB = await _contexto.Choferes.FindAsync(Chofer.CI);
                Chofer choferConMatriculaIgual = await _contexto.Choferes
                    .Where(chofer => chofer.CI != Chofer.CI && chofer.Vehiculo == Chofer.Vehiculo && chofer.Vehiculo != "Sin asignar")
                    .FirstOrDefaultAsync();


                if (ChoferDesdeDB != null && choferConMatriculaIgual == null){
                    ChoferDesdeDB.CI = Chofer.CI;
                    ChoferDesdeDB.Nombre = Chofer.Nombre;
                    ChoferDesdeDB.Apellido = Chofer.Apellido;
                    ChoferDesdeDB.Edad = Chofer.Edad;
                    ChoferDesdeDB.Vehiculo = Chofer.Vehiculo;
                    await _contexto.SaveChangesAsync();
                    Chofer.Modificado = true;  // Agregue aquí este código para cambiar el estado
                    Notificacion.Estado = true;
                    Mensaje = "Datos modificados con éxito.";
                    Notificacion.Mensaje = Mensaje;
                    return RedirectToPage("Choferes");
                }else if(choferConMatriculaIgual != null){
                    ModelState.AddModelError("Chofer.Vehiculo", $"Ya existe un usuario con la matricula : {Chofer.Vehiculo} asignada");
                    
                }else{
                return NotFound();
                }
            }

            Vehiculos = await _contexto.Vehiculos.ToListAsync();
            return Page();
        }
    }
}

    


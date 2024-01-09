using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObligatorioParteII.Datos;
using ObligatorioParteII.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ObligatorioParteII.Pages.Listados{
    public class ListadosChoferModel : PageModel{


        private readonly ApplicationDbContextt _contexto;
        public IEnumerable<Chofer> Choferes { get; set; }


        public ListadosChoferModel(ApplicationDbContextt contexto){
            _contexto = contexto;
        }
        public async Task OnGet(){ 
            Choferes = await _contexto.Choferes.ToListAsync();
        }
     
    }
}

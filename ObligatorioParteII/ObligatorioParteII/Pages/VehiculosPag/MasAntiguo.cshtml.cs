using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObligatorioParteII.Datos;
using ObligatorioParteII.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ObligatorioParteII.Pages.VehiculosPag{
    public class MasAntiguoModel : PageModel{


        private readonly ApplicationDbContextt _contexto;
        public Vehiculo Vehiculos { get; set; }


        public MasAntiguoModel(ApplicationDbContextt contexto){
            _contexto = contexto;
        }
        public async Task OnGetAsync(){
            Vehiculos = await _contexto.ObtenerMasAntiguoAsync();
        }
    }
}

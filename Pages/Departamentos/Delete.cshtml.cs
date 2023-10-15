using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Departamentos
{
    public class DeleteModel : PageModel
    {
        private readonly InventariosContext _context;
        private readonly INotyfService _servicoNotificacion;

        public DeleteModel( InventariosContext context,INotyfService servicoNotificacion)
        {
            _context = context;
            _servicoNotificacion = servicoNotificacion;
        }

        [BindProperty]
      public Departamento Departamento { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                _servicoNotificacion.Warning($"El identificador debe tener un valor.");
                return NotFound();
            }

            var departamento = await _context.Departamentos.FirstOrDefaultAsync(m => m.Id == id);

            if (departamento == null)
            {
                _servicoNotificacion.Warning($"No se ha encontrado la departamento con el identificador aproiado.");
                return NotFound();
            }
            else 
            {
                Departamento = departamento;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.FindAsync(id);

            if (departamento != null)
            {
                Departamento = departamento;
                _context.Departamentos.Remove(Departamento);
                await _context.SaveChangesAsync();
            }
            _servicoNotificacion.Success($"EXITO al eliminar el departamento {Departamento.Nombre}");
            return RedirectToPage("./Index");
        }
    }
}

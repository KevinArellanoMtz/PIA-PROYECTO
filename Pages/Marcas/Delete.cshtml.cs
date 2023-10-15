using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas;

public class DeleteModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly INotyfService _servicoNotificacion;

    public DeleteModel(InventariosContext context, INotyfService servicoNotificacion)
    {
        _context = context;
        _servicoNotificacion = servicoNotificacion;
    }

    [BindProperty]
  public Marca Marca { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Marcas == null)
        {
            _servicoNotificacion.Warning($"El identificador debe tener un valor.");
            return NotFound();
        }

        var marca = await _context.Marcas.FirstOrDefaultAsync(m => m.Id == id);

        if (marca == null)
        {
            _servicoNotificacion.Warning($"No se ha encontrado la marca con el identificador aproiado.");
            return NotFound();
        }
        else 
        {
            Marca = marca;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _context.Marcas == null)
        {
            return NotFound();
        }
        var marca = await _context.Marcas.FindAsync(id);

        if (marca != null)
        {
            Marca = marca;
            _context.Marcas.Remove(Marca);
            await _context.SaveChangesAsync();
        }
        _servicoNotificacion.Success($"EXITO al eliminar la marca {Marca.Nombre}");
        return RedirectToPage("./Index");
    }
}

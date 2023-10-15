using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Marcas;

public class EditModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly INotyfService _servicoNotificacion;

    public EditModel(InventariosContext context, INotyfService servicoNotificacion)
    {
        _context = context;
        _servicoNotificacion = servicoNotificacion;
    }

    [BindProperty]
    public Marca Marca { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Marcas == null)
        {
            _servicoNotificacion.Warning($"El identificador de la marca debe tener un valor diferente a nulo.");
            return NotFound();
        }

        var marca =  await _context.Marcas.FirstOrDefaultAsync(m => m.Id == id);
        if (marca == null)
        {
            _servicoNotificacion.Warning($"No se ha encontrado la marca con el identificador");
            return NotFound();
        }
        Marca = marca;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            _servicoNotificacion.Error($"Es necesario corregir los problemas para poder editar la marca.");
            return Page();
        }

        var existeMarcaBd = _context.Marcas.Any(u => u.Nombre.ToLower().Trim() == Marca.Nombre.ToLower().Trim()
                                                            &&u.Id!=Marca.Id);
        if (existeMarcaBd)
        {
            _servicoNotificacion.Warning($"Ya existe una marca con el nombre{Marca.Nombre}");
            return Page();
        }


        _context.Attach(Marca).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MarcaExists(Marca.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        _servicoNotificacion.Success($"EXITO al actualizar la marca{Marca.Nombre}.");
        return RedirectToPage("./Index");
    }

    private bool MarcaExists(int id)
    {
      return _context.Marcas.Any(e => e.Id == id);
    }
}

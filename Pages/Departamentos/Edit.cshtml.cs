using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Departamentos;

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
    public Departamento Departamento { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Departamentos == null)
        {
            _servicoNotificacion.Warning($"El identificador del departamento debe tener un valor diferente a nulo.");

            return NotFound();
        }

        var departamento =  await _context.Departamentos.FirstOrDefaultAsync(m => m.Id == id);
        if (departamento == null)
        {
            _servicoNotificacion.Warning($"No se ha encontrado el departamento con ese identificador");
            return NotFound();
        }
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            _servicoNotificacion.Error($"Es necesario corregir los problemas para poder editar el departamento.");

            return Page();
        }
        var existeDepartamentoBd = _context.Departamentos.Any(u => u.Nombre.ToLower().Trim() == Departamento.Nombre.ToLower().Trim()
                                                           && u.Id != Departamento.Id);
        if (existeDepartamentoBd)
        {
            _servicoNotificacion.Warning($"Ya existe una marca con el nombre{Departamento.Nombre}");
            return Page();
        }
       
        _context.Attach(Departamento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartamentoExists(Departamento.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        _servicoNotificacion.Success($"EXITO al actualizar la marca{Departamento.Nombre}.");
        return RedirectToPage("./Index");
    }

    private bool DepartamentoExists(int id)
    {
      return _context.Departamentos.Any(e => e.Id == id);
    }
}

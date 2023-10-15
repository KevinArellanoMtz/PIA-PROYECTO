using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPInventarios.Data;
using RPInventarios.Models;

namespace RPInventarios.Pages.Departamentos;

public class CreateModel : PageModel
{
    private readonly InventariosContext _context;
    private readonly INotyfService _servicoNotificacion;


    public CreateModel(InventariosContext context, INotyfService servicoNotificacion)
    {
        _context = context;
        _servicoNotificacion = servicoNotificacion;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Departamento Departamento { get; set; }
    

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
            _servicoNotificacion.Error($"Es necesario corregir los problemas para poder crear la marca{Departamento.Nombre}");
            return Page();
      }
      var existeDepartamentoBd = _context.Departamentos.Any(u => u.Nombre.ToLower().Trim() == Departamento.Nombre.ToLower().Trim());
      if (existeDepartamentoBd)
      {
            _servicoNotificacion.Warning($"Ya existe una marca con el nombre {Departamento.Nombre}");
            return Page();
      }

        _context.Departamentos.Add(Departamento);
        await _context.SaveChangesAsync();
        _servicoNotificacion.Success($"EXITO al crear la marca {Departamento.Nombre}");

        return RedirectToPage("./Index");
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RPInventarios.Models;

public class Departamento
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    [Required(ErrorMessage = "El nombre es requerido")]
    [MinLength(4,ErrorMessage ="Minimo 4 caracteres")]
    [MaxLength(100,ErrorMessage ="Maximo 100 caracteres")]
    [Display(Name = "Departamento")]

    public string Descripcion { get; set; }
    [MinLength(5,ErrorMessage ="Minimo 5 caracteres")]
    [MaxLength(200,ErrorMessage ="Maximo 200 caracteres")]

    //public List<DateTime> FechaCreacion { get; set; }


    
    public virtual ICollection<Producto> Productos { get; set; }
}

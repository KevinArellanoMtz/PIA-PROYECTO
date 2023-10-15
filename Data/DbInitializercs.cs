using RPInventarios.Models;
using System.Xml.Xsl;

namespace RPInventarios.Data;

public class DbInitializer
{
    public static void Initialize(InventariosContext context)
    {
        //Checamos si existe alguna marca
        if (context.Marcas.Any())
        {
            return; //La BD ha sido iniciada con informacion
        }
        var marcas = new Marca[]
        {
            new Marca{Nombre="Rino"},
            new Marca{Nombre="Rocco"},
            new Marca{Nombre="Azuri"},
            new Marca{Nombre="Reni"},
            new Marca{Nombre="Bazi"},
            new Marca{Nombre="Asis"}
        };

        context.Marcas.AddRange(marcas);
        context.SaveChanges();

        var departamentos = new Departamento[]
        {
            new Departamento{Nombre="Administracion General",
                Descripcion="Recursos General"},

            new Departamento{Nombre="Recursos Humanos",
                Descripcion="Recursos Humanos"},

            new Departamento{Nombre="Recursos Materiales",
                Descripcion="Recursos Materiales"},

            new Departamento{Nombre="Informatica",
                Descripcion="Informatica"},

            new Departamento{Nombre="Deportes",
                Descripcion="Deportes"},

            new Departamento{Nombre="Asis",
                Descripcion="Asis"}
        };

        context.Departamentos.AddRange(departamentos);
        context.SaveChanges();

        var productos = new Producto[]
       {
            new Producto{
                Nombre="Silla Secretarial",
                Descripcion="Silla de imitacion de piel",
                MarcaId = context.Marcas.First(u=>u.Nombre=="Rino").Id,
                 Costo=2500
            },
            new Producto{
                Nombre="Escritorio General",
                Descripcion="Escritorio negro con cristal templado",
                MarcaId = context.Marcas.First(u=>u.Nombre=="Azuri").Id,
                 Costo=2500
            },
            new Producto{
                Nombre="Cafetera Industrial",
                Descripcion="Cafeteria paara 50 tazas",
                MarcaId = context.Marcas.First(u=>u.Nombre=="Rocco").Id,
                 Costo=2500
            },
            new Producto{
                Nombre="Computadora",
                Descripcion="Computadora Gamer",
                MarcaId = context.Marcas.First(u=>u.Nombre=="Asis").Id,
                 Costo=65500
            },
            new Producto{
                Nombre="Proyector",
                Descripcion="Proyector Inalambrico",
                MarcaId = context.Marcas.First(u=>u.Nombre=="Reni").Id,
                 Costo=65500
            },
       };


        context.Productos.AddRange(productos);
        context.SaveChanges();
    }
}

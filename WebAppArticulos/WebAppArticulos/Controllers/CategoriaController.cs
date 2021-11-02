using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppArticulos.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppArticulos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ArticuloContext _context;

        public CategoriaController(ArticuloContext context)
        {
            _context = context;
        }
        // GET: api/<CategoriaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //creamos una variable donde incluiremos los productos a cada categoria
                var categorias = _context.Categorias
                    .Include(cat => cat.Productos)
                    .ToList();

                //creamos una variable anónima donde agregaramos los productos de las categorias a devolver
                var catProductos = categorias.Select(cat => new
                {
                    Id = cat.Id,
                    Nombre = cat.Nombre,
                    Descripcion = cat.Descripcion,
                    Producto = cat.Productos.Select(prod=> new
                    {
                        Id = prod.Id,
                        Nombre = prod.Nombre,
                        Descripcion = prod.Descripcion,
                        FechaCreacion = prod.FechaCreacion,
                        FechaModificacion = prod.FechaModificacion,
                        Codigo = prod.Codigo,
                        PrecioUnitario = prod.PrecioUnitario,
                        Stock = prod.Stock,
                    }).ToList()
                }).ToList();
                
                //retornamos
                return Ok(catProductos);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

    }
}

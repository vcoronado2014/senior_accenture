using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppArticulos.DAO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppArticulos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosDaoController : ControllerBase
    {
        private readonly ILogger<ProductosDao> _logger;
        public ProductosDaoController(ILogger<ProductosDao> logger)
        {
            _logger = logger;
        }
        // GET: api/<ProductosDaoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                DAO.ProductosDao dao = new DAO.ProductosDao(_logger);
                var productos = dao.ObtenerProductos();
                return Ok(productos);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

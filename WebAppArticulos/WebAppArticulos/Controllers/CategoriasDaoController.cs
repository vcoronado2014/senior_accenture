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
    public class CategoriasDaoController : ControllerBase
    {
        private readonly ILogger<ProductosDao> _logger;
        public CategoriasDaoController(ILogger<ProductosDao> logger)
        {
            _logger = logger;
        }
        // GET: api/<CategoriasDaoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                DAO.ProductosDao dao = new DAO.ProductosDao(_logger);
                var categorias = dao.ObtenerCategorias();
                return Ok(categorias);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

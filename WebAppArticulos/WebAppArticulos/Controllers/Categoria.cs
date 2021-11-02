using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppArticulos.Controllers
{
    public class Categoria
    {
        public long Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Eliminado { get; set; }

        public List<Producto> Productos { get; set; }
    }
}

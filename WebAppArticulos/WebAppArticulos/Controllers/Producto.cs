using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppArticulos.Controllers
{
    public class Producto
    {
        public long Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string Codigo { get; set; }

        public int PrecioUnitario { get; set; }

        public int Stock { get; set; }

        public int Eliminado { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
    }
}

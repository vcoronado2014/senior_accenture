using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppArticulos.DAO
{
    public class ProductosDao 
    {
        private readonly ILogger<ProductosDao> _logger;

        public ProductosDao(ILogger<ProductosDao> logger)
        {
            _logger = logger;
        }
        public List<Models.Producto> ObtenerProductos()
        {
            List<Models.Producto> productos = new List<Models.Producto>();
            //conexión a la base de datos
            SqlConnection conn = ConexionBD.connection;
            //comando para ejecutar un procedimiento
            SqlCommand cmd = new SqlCommand("OBTENER_PRODUCTOS", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //abrimos la conexión a la base de datos
            conn.Open();

            try
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                int ID = rdr.GetOrdinal("ID");
                int NOMBRE = rdr.GetOrdinal("NOMBRE");
                int DESCRIPCION = rdr.GetOrdinal("DESCRIPCION");
                int FECHA_CREACION = rdr.GetOrdinal("FECHACREACION");
                int FECHA_MODIFICACION = rdr.GetOrdinal("FECHAMODIFICACION");
                int CODIGO = rdr.GetOrdinal("CODIGO");
                int PRECIO_UNITARIO = rdr.GetOrdinal("PRECIOUNITARIO");
                int STOCK = rdr.GetOrdinal("STOCK");
                int ELIMINADO = rdr.GetOrdinal("ELIMINADO");
                int CATEGORIA_ID = rdr.GetOrdinal("CATEGORIAID");
                try
                {
                    while (rdr.Read())
                    {
                        Models.Producto producto = new Models.Producto();
                        producto.Id = rdr.IsDBNull(ID) ? 0 : rdr.GetInt64(ID);
                        producto.Nombre = rdr.IsDBNull(NOMBRE) ? "" : rdr.GetString(NOMBRE);
                        producto.Descripcion = rdr.IsDBNull(DESCRIPCION) ? "" : rdr.GetString(DESCRIPCION);
                        producto.FechaCreacion = rdr.IsDBNull(FECHA_CREACION) ? DateTime.MinValue : rdr.GetDateTime(FECHA_CREACION);
                        producto.FechaModificacion = rdr.IsDBNull(FECHA_MODIFICACION) ? DateTime.MinValue : rdr.GetDateTime(FECHA_MODIFICACION);
                        producto.Codigo = rdr.IsDBNull(CODIGO) ? "" : rdr.GetString(CODIGO);
                        producto.PrecioUnitario = rdr.IsDBNull(PRECIO_UNITARIO) ? 0 : rdr.GetInt32(PRECIO_UNITARIO);
                        producto.Stock = rdr.IsDBNull(STOCK) ? 0 : rdr.GetInt32(STOCK);
                        producto.Eliminado = rdr.IsDBNull(ELIMINADO) ? 1 : rdr.GetInt32(ELIMINADO);
                        producto.CategoriaId = rdr.IsDBNull(CATEGORIA_ID) ? 0 : rdr.GetInt64(CATEGORIA_ID);

                        //agregamos
                        productos.Add(producto);
                    }
                }
                finally
                {
                    rdr.Close();
                }
            }
            catch(Exception ex)
            {
                //excepcion
                _logger.LogError(ex.Message);

            }
            finally
            {
                conn.Close();
            }

            return productos;
        }
    }
}

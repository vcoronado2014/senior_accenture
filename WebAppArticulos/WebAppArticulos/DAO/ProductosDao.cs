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

        public List<Models.Categoria> ObtenerCategorias()
        {
            List<Models.Categoria> categorias = new List<Models.Categoria>();
            var cat = new List<object>();
            //conexión a la base de datos
            SqlConnection conn = ConexionBD.connection;
            //comando para ejecutar un procedimiento
            SqlCommand cmd = new SqlCommand("OBTENER_CATEGORIAS", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //abrimos la conexión a la base de datos
            conn.Open();

            try
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                int ID = rdr.GetOrdinal("ID");
                int NOMBRE = rdr.GetOrdinal("NOMBRE");
                int PROD_ID = rdr.GetOrdinal("PROD_ID");
                int PROD_NOMBRE = rdr.GetOrdinal("PROD_NOMBRE");
                int PROD_DESCRIPCION = rdr.GetOrdinal("PROD_DESCRIPCION");
                int DESCRIPCION = rdr.GetOrdinal("DESCRIPCION");
                int FECHA_CREACION = rdr.GetOrdinal("FECHACREACION");
                int FECHA_MODIFICACION = rdr.GetOrdinal("FECHAMODIFICACION");
                int CODIGO = rdr.GetOrdinal("CODIGO");
                int PRECIO_UNITARIO = rdr.GetOrdinal("PRECIOUNITARIO");
                int STOCK = rdr.GetOrdinal("STOCK");
                int CATEGORIA_ID = rdr.GetOrdinal("CATEGORIAID");
                int ELIMINADO = rdr.GetOrdinal("ELIMINADO");
                int PROD_ELIMINADO = rdr.GetOrdinal("PROD_ELIMINADO");

                try
                {
                    while (rdr.Read())
                    {

                        long id = rdr.IsDBNull(ID) ? 0 : rdr.GetInt64(ID);
                        Models.Categoria categoria;
                        Models.Categoria categoriaBuscar = categorias.FirstOrDefault(categoria => categoria.Id == id);
                        if (categoriaBuscar == null)
                        {
                            categoria = new Models.Categoria
                            {
                                Descripcion = rdr.IsDBNull(DESCRIPCION) ? "" : rdr.GetString(DESCRIPCION),
                                Id = rdr.IsDBNull(ID) ? 0 : rdr.GetInt64(ID),
                                Nombre = rdr.IsDBNull(NOMBRE) ? "" : rdr.GetString(NOMBRE),
                                Eliminado = rdr.IsDBNull(ELIMINADO) ? "0" : rdr.GetString(ELIMINADO),
                            };
                            categoria.Productos = new List<Models.Producto>();
                            Models.Producto producto = new Models.Producto();
                            producto.CategoriaId = id;
                            producto.Codigo = rdr.IsDBNull(CODIGO) ? "" : rdr.GetString(CODIGO);
                            producto.Descripcion = rdr.IsDBNull(PROD_DESCRIPCION) ? "" : rdr.GetString(PROD_DESCRIPCION);
                            producto.FechaCreacion = rdr.IsDBNull(FECHA_CREACION) ? DateTime.MinValue : rdr.GetDateTime(FECHA_CREACION);
                            producto.FechaModificacion = rdr.IsDBNull(FECHA_MODIFICACION) ? DateTime.MinValue : rdr.GetDateTime(FECHA_MODIFICACION);
                            producto.Id = rdr.IsDBNull(PROD_ID) ? 0 : rdr.GetInt64(PROD_ID);
                            producto.Nombre = rdr.IsDBNull(PROD_NOMBRE) ? "" : rdr.GetString(PROD_NOMBRE);
                            producto.PrecioUnitario = rdr.IsDBNull(PRECIO_UNITARIO) ? 0 : rdr.GetInt32(PRECIO_UNITARIO);
                            producto.Stock = rdr.IsDBNull(STOCK) ? 0 : rdr.GetInt32(STOCK);
                            producto.Eliminado = rdr.IsDBNull(PROD_ELIMINADO) ? 0 : rdr.GetInt32(PROD_ELIMINADO);
                            categoria.Productos.Add(producto);

                            categorias.Add(categoria);
                        }
                        else
                        {
                            categoria = categoriaBuscar;
                            if (categoriaBuscar.Productos == null)
                            {
                                categoria.Productos = new List<Models.Producto>();
                            }
                            
                            Models.Producto producto = new Models.Producto();
                            producto.CategoriaId = id;
                            producto.Codigo = rdr.IsDBNull(CODIGO) ? "" : rdr.GetString(CODIGO);
                            producto.Descripcion = rdr.IsDBNull(PROD_DESCRIPCION) ? "" : rdr.GetString(PROD_DESCRIPCION);
                            producto.FechaCreacion = rdr.IsDBNull(FECHA_CREACION) ? DateTime.MinValue : rdr.GetDateTime(FECHA_CREACION);
                            producto.FechaModificacion = rdr.IsDBNull(FECHA_MODIFICACION) ? DateTime.MinValue : rdr.GetDateTime(FECHA_MODIFICACION);
                            producto.Id = rdr.IsDBNull(PROD_ID) ? 0 : rdr.GetInt64(PROD_ID);
                            producto.Nombre = rdr.IsDBNull(PROD_NOMBRE) ? "" : rdr.GetString(PROD_NOMBRE);
                            producto.PrecioUnitario = rdr.IsDBNull(PRECIO_UNITARIO) ? 0 : rdr.GetInt32(PRECIO_UNITARIO);
                            producto.Stock = rdr.IsDBNull(STOCK) ? 0 : rdr.GetInt32(STOCK);
                            producto.Eliminado = rdr.IsDBNull(PROD_ELIMINADO) ? 0 : rdr.GetInt32(PROD_ELIMINADO);
                            categoria.Productos.Add(producto);
                        }

                        

                    }
                }
                finally
                {
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                //excepcion
                _logger.LogError(ex.Message);

            }
            finally
            {
                conn.Close();
            }


            return categorias;
        }
    }
}

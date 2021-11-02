using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppArticulos.DAO
{
    public static class ConexionBD
    {
        private const string CADENA = "Server=LAPTOP-FPESQ3PP\\SQLEXPRESS;Database=ArticulosDBAccenture;Trusted_Connection=True;User Id=sa;Password=co2005";
        public static SqlConnection connection = new SqlConnection(CADENA);
    }
}

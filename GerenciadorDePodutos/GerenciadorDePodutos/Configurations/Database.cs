using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorDePodutos.Configurations
{
    public class Database
    {
        public static string getConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["GerenciadorProdutos"].ConnectionString;
        }
    }
}
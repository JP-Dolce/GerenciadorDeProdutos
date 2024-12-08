using GerenciadorDePodutos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GerenciadorDePodutos.Repositories
{
    public class Categoria
    {
        private SqlConnection _conexao;
        private readonly SqlCommand _cmd;
        public Categoria(string conexao)
        {
            _conexao = new SqlConnection(conexao);
            _cmd = new SqlCommand();
            _cmd.Connection = _conexao;
        }

        public Models.Categoria SelectByName(string nome)
        {
            Models.Categoria categoria = null;
            using (_conexao)
            {
                _conexao.Open();
                using (_cmd) 
                {
                    _cmd.Parameters.Clear();
                    _cmd.CommandText = "SELECT * FROM Categorias WHERE Nome = @Nome";
                    _cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = $"%{nome}%";

                    using(SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            categoria = new Models.Categoria
                            {
                                Id = (int)dr["id"],
                                Nome = dr["nome"].ToString()
                            }; 
                        }
                    }
                }
            }
            return categoria;
        }
        public bool CriarCategoria(Models.Categoria categoria)
        {

            using (var _conexao = new SqlConnection("Server = DESKTOP-VCS032K\\SQLEXPRESS; Database = GerenciadorProdutos; Trusted_Connection = True;"))
            {
                _conexao.Open();
                using (_cmd)
                {
                    _cmd.Parameters.Clear();
                    _cmd.Connection = _conexao;
                    _cmd.CommandText = "INSERT INTO Categorias (Nome) OUTPUT INSERTED.Id VALUES (@Nome)";

                    _cmd.Parameters.Add(new SqlParameter("@Nome", System.Data.SqlDbType.VarChar)).Value = categoria.Nome;
                    categoria.Id = Convert.ToInt32(_cmd.ExecuteScalar());
                }
            }
            return categoria.Id != 0;
        }
    }
}
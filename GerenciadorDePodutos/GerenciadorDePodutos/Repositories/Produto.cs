using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GerenciadorDePodutos.Repositories
{
    public class Produto
    {
        private readonly SqlConnection _conexao;
        private readonly SqlCommand _cmd;
        public Produto(string conexao)
        {
            _conexao = new SqlConnection(conexao);
            _cmd = new SqlCommand();
            _cmd.Connection = _conexao;
        }
        public List<Models.Produto> SelectAll() 
        {
            List<Models.Produto> produtos = new List<Models.Produto>();

            using (_conexao)
            {
                _conexao.Open();
                using (_cmd)
                {
                    _cmd.CommandText = "SELECT Id, Nome, Descricao, Status, Preco, QuantidadeEstoque, CategoriaNome FROM Produtos";
                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Models.Produto produto = new Models.Produto();
                            produto.Id = (int)dr["Id"];
                            produto.Nome = dr["Nome"].ToString();
                            produto.Descricao = dr["Descricao"].ToString();
                            produto.Status = dr["Status"].ToString();
                            produto.Preco = (decimal)dr["Preco"];
                            produto.QuantidadeEstoque = (int)dr["QuantidadeEstoque"];
                            produto.CategoriaNome = dr["CategoriaNome"].ToString();
                            produtos.Add(produto);  
                        }
                        return produtos;
                    }
                }
            }
        }
        public Models.Produto SelectById(int id)
        {
            Models.Produto produto = new Models.Produto();
            using (_conexao)
            {
                _conexao.Open();
                using (_cmd)
                {
                    _cmd.CommandText = "SELECT Id, Nome, Descricao, Status, Preco, QuantidadeEstoque, CategoriaNome FROM Produtos WHERE Id = @Id;";
                    _cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            produto.Id = (int)dr["Id"];
                            produto.Nome = dr["Nome"].ToString();
                            produto.Descricao = dr["Descricao"].ToString();
                            produto.Status = dr["Status"].ToString();
                            produto.Preco = (decimal)dr["Preco"];
                            produto.QuantidadeEstoque = (int)dr["QuantidadeEstoque"];
                            produto.CategoriaNome = dr["CategoriaNome"].ToString();
                        }
                    }
                    if (produto.Id == 0)
                        return null;
                }
            }
            return produto;
        }
        public List<Models.Produto> SelectByStatus(string status)
        {
            List<Models.Produto> produtos = new List<Models.Produto>();
            using (_conexao)
            {
                _conexao.Open();
                using (_cmd)
                {
                    _cmd.CommandText = "SELECT Id, Nome, Descricao, Status, Preco, QuantidadeEstoque, CategoriaNome FROM Produtos WHERE Status = @Status;";
                    _cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar)).Value = status;
                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Models.Produto produto = new Models.Produto();
                            produto.Id = (int)dr["Id"];
                            produto.Nome = dr["Nome"].ToString();
                            produto.Descricao = dr["Descricao"].ToString();
                            produto.Status = dr["Status"].ToString();
                            produto.Preco = (decimal)dr["Preco"];
                            produto.QuantidadeEstoque = (int)dr["QuantidadeEstoque"];
                            produto.CategoriaNome = dr["CategoriaNome"].ToString();
                            produtos.Add(produto);
                        }
                    }
                }
            }
            return produtos;
        }
        
        public bool CriarProduto(Models.Produto produto)
        {
          
            using (_conexao)
            {
                _conexao.Open();
                using (_cmd)
                {
                    _cmd.Connection = _conexao;
                    _cmd.CommandText = $"INSERT INTO Produtos (Nome, Descricao, Preco, QuantidadeEstoque, CategoriaId) VALUES (@Nome, @Descricao, @Preco, @QuantidadeEstoque, @CategoriaId); select convert(int,SCOPE_IDENTITY()) as Id";
                    _cmd.Parameters.Add(new SqlParameter("@Nome", System.Data.SqlDbType.VarChar)).Value = produto.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@Descricao", System.Data.SqlDbType.VarChar)).Value = produto.Descricao;
                    _cmd.Parameters.Add(new SqlParameter("@Preco", System.Data.SqlDbType.Decimal)).Value = produto.Preco;
                    _cmd.Parameters.Add(new SqlParameter("@QuantidadeEstoque", System.Data.SqlDbType.Int)).Value = produto.QuantidadeEstoque;
                    _cmd.Parameters.Add(new SqlParameter("@CategoriaId", System.Data.SqlDbType.Int)).Value = produto.CategoriaId;
                   
                    produto.Id = Convert.ToInt32(_cmd.ExecuteScalar());
                }
            }
            return produto.Id != 0;
        }
        public bool UpdateProdutos(Models.Produto produto)
        {
            int linhasAfetadas = 0;
            using (_conexao)
            {
                _conexao.Open();
                using (_cmd) 
                {
                    _cmd.Connection = _conexao;
                    _cmd.CommandText = $"UPDATE Produtos SET Nome = @Nome, Descricao = @Descricao, Preco = @Preco, QuantidadeEstoque = @QuantidadeEstoque, CategoriaNome = @CategoriaNome WHERE Id = @Id";
                    _cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int)).Value = produto.Id;
                    _cmd.Parameters.Add(new SqlParameter("@Nome", System.Data.SqlDbType.VarChar)).Value = produto.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@Descricao", System.Data.SqlDbType.VarChar)).Value = produto.Descricao;
                    _cmd.Parameters.Add(new SqlParameter("@Preco", System.Data.SqlDbType.Decimal)).Value = produto.Preco;
                    _cmd.Parameters.Add(new SqlParameter("@QuantidadeEstoque", System.Data.SqlDbType.Int)).Value = produto.QuantidadeEstoque;
                    _cmd.Parameters.Add(new SqlParameter("@CategoriaNome", System.Data.SqlDbType.VarChar)).Value = produto.CategoriaNome;
                    linhasAfetadas = _cmd.ExecuteNonQuery();
                }
            }
            return linhasAfetadas != 0;
        }
        public bool DeleteById(int id)
        {
            int linhasAfetadas = 0;
            using (_conexao)
            {

                _conexao.Open();

                string sql = $"DELETE FROM Produtos WHERE Id = @Id";
                using (_cmd)
                {
                    _cmd.Connection = _conexao;
                    _cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                    _cmd.CommandText = sql;
                    linhasAfetadas = _cmd.ExecuteNonQuery();

                }
                return linhasAfetadas == 1;
            }
        }
    }

}
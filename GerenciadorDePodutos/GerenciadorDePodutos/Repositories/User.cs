using GerenciadorDePodutos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GerenciadorDePodutos.Repositories
{
    public class User
    {
        private readonly SqlConnection _conexao;
        private readonly SqlCommand _cmd;
        public User(string conexao)
        {
            _conexao = new SqlConnection(conexao);
            _cmd = new SqlCommand();
            _cmd.Connection = _conexao;
        }
        public List<Models.Usuarios> SelectAll()
        {
            List<Models.Usuarios> users = new List<Models.Usuarios>();

            using (_conexao)
            {
                _conexao.Open();
                using (_cmd)
                {
                    _cmd.CommandText = "SELECT Id, Nome, Senha, Papel FROM Usuarios";
                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Models.Usuarios user = new Models.Usuarios();
                            user.Id = (int)dr["Id"];
                            user.Nome = dr["Nome"].ToString();
                            user.Senha = dr["Senha"].ToString();
                            user.Papel = dr["Papel"].ToString();
                            
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }
        }
        [Route("api/Login")]
        public Models.Usuarios Select(string nome, string senha)
        {
            Models.Usuarios user = null;

            using (_conexao)
            {
                _conexao.Open();
                using (_cmd)
                {
                   
                    _cmd.CommandText = "SELECT Id, Nome, Senha, Papel FROM Usuarios WHERE Nome = @Nome AND Senha = @Senha";
                    _cmd.Parameters.AddWithValue("@Nome", nome);
                    _cmd.Parameters.AddWithValue("@Senha", senha);

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        if (dr.Read()) 
                        {
                            user = new Models.Usuarios
                            {
                                Id = (int)dr["Id"],
                                Nome = dr["Nome"].ToString(),
                                Senha = dr["Senha"].ToString(),
                                Papel = dr["Papel"].ToString()
                            };
                        }
                    }
                }
            }
            return user;
        }

        public bool CreateUser(Models.Usuarios user) 
        {
            using (_conexao)
            {
                _conexao.Open();
                using (_cmd)
                {
                    _cmd.Connection = _conexao;
                    _cmd.CommandText = $"INSERT INTO Usuarios(Nome, Senha, Papel) values (@Nome, @Senha, @Papel); select convert(int,SCOPE_IDENTITY()) as Id";
                    _cmd.Parameters.Add(new SqlParameter("@Nome", System.Data.SqlDbType.VarChar)).Value = user.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@Senha", System.Data.SqlDbType.VarChar)).Value = user.Senha;
                    _cmd.Parameters.Add(new SqlParameter("@Papel", System.Data.SqlDbType.VarChar)).Value = user.Papel;
                    user.Id = Convert.ToInt32(_cmd.ExecuteScalar());
                }
            }
            return user.Id != 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GerenciadorDePodutos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProdutosController : ApiController
    {
        private readonly Repositories.Produto _repoProduto;
        private readonly Repositories.Categoria _repoCategoria;
        public ProdutosController()
        {
            var conexao = Configurations.Database.getConnectionString();
            _repoProduto = new Repositories.Produto(conexao);
            _repoCategoria = new Repositories.Categoria(conexao);
        }
        // GET: api/Produtos
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Models.Produto> produtos = _repoProduto.SelectAll();
            if (produtos != null)
            {
                if (produtos.Count == 0)
                    return BadRequest("Produtos não encontrados!");
                return Ok(produtos);
            }
            else
                return InternalServerError();
        }
        // GET: api/Produtos
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            Models.Produto produto = _repoProduto.SelectById(id);
            if (produto == null)
                return BadRequest("Produto não encontrado, certifique-se de que o ID do produto esteja correto.");
            return Ok(produto);
        }

        // GET: api/Produtos
        [HttpGet]
        public IHttpActionResult GetStatus(string status)
        {
            List<Models.Produto> produtos = _repoProduto.SelectByStatus(status);
            if (produtos != null)
            {
                if (produtos.Count == 0)
                    return BadRequest("Não há produto com esse status!");
                return Ok(produtos);
            }
            return InternalServerError();
        }

        // POST: api/Produtos
        [HttpPost]
        public IHttpActionResult CriarProduto([FromBody] Models.Produto produto)
        {
            if (produto == null)
                return BadRequest("Dados do produto não fornecidos.");

            var categoriaExistente = _repoCategoria.SelectByName(produto.CategoriaNome);
            if (categoriaExistente == null)
            {
                var novaCategoria = new Models.Categoria { Nome = produto.CategoriaNome };
                if (!_repoCategoria.CriarCategoria(novaCategoria))
                    return InternalServerError();
                produto.CategoriaId = novaCategoria.Id;
            }
            else
            {
                produto.CategoriaId = categoriaExistente.Id;
            }
            produto.Status = produto.QuantidadeEstoque > 0 ? "Em estoque" : "Indisponível";

            if (!this._repoProduto.CriarProduto(produto))
                return BadRequest("Não foi possível criar o produto!");

            return Ok("Produto criado com sucesso!");
        }

        // PUT: api/Produtos/5
        [HttpPut]
        public IHttpActionResult Put(int id, Models.Produto produto)
        {
            if (id != produto.Id)
                return BadRequest("O ID da requisição não coincide com o ID do produto!");
            if (!_repoProduto.UpdateProdutos(produto))
                return BadRequest("Não foi possível alterar o Produto!");

            return Ok("Produto alterado com sucesso!");
        }

        // DELETE: api/Produtos/5
        [HttpDelete]
        public IHttpActionResult DeleteById(int id)
        {
            if (!_repoProduto.DeleteById(id))
                return BadRequest("Produto não encontrado!");
            return Ok($"Perfume ID:{id} deletado com sucesso!");
        }
    }
}

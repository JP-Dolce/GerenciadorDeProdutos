using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorDePodutos.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string CategoriaNome { get; set; }
        public int CategoriaId { get; set; }
    }
}
# Sistema de Gestão de Produtos e Categorias

Este repositório contém o código de um sistema de gestão de categorias, produtos e usuários, utilizando C# com SQL Server para persistência de dados. O sistema oferece funcionalidades básicas de CRUD (Criar, Ler, Atualizar, Deletar) para essas entidades.

## Estrutura do Projeto

O projeto é dividido em três principais repositórios:

1. **CategoriaRepository**: Gerencia as operações de CRUD para a tabela `Categorias`.
2. **ProdutoRepository**: Gerencia as operações de CRUD para a tabela `Produtos`.
3. **UserRepository**: Gerencia as operações de CRUD para a tabela `Usuarios`.

### Requisitos

Antes de começar, certifique-se de ter os seguintes requisitos:

- **.NET 6.0 ou superior** instalado.
- **SQL Server** instalado e configurado.
- Conexão com o banco de dados configurada corretamente.

### Instalação

1. Clone este repositório para a sua máquina local:

    ```bash
    git clone https://github.com/seu-usuario/nome-do-repositorio.git
    ```

2. Abra o projeto no Visual Studio ou em sua IDE preferida.

3. Altere a string de conexão para o banco de dados no código, substituindo a variável `string_conexao` pelo endereço do seu banco de dados SQL Server.

### Uso

#### Conexão com o Banco de Dados

A classe `CategoriaRepository`, `ProdutoRepository` e `UserRepository` utilizam uma string de conexão para interagir com o banco de dados. A conexão deve ser configurada no parâmetro de inicialização do repositório.

```csharp
Categoria categoriaRepo = new Categoria("string_conexao");
Produto produtoRepo = new Produto("string_conexao");
User userRepo = new User("string_conexao");
Substitua "string_conexao" pela sua string de conexão do SQL Server.

Criar Nova Categoria
Para criar uma nova categoria:

csharp
Copiar código
Categoria categoriaRepo = new Categoria("string_conexao");
Models.Categoria novaCategoria = new Models.Categoria { Nome = "Eletrônicos" };
bool sucesso = categoriaRepo.CriarCategoria(novaCategoria);
Buscar Categoria por Nome
Para buscar uma categoria pelo nome:

csharp
Copiar código
Categoria categoriaRepo = new Categoria("string_conexao");
Models.Categoria categoria = categoriaRepo.SelectByName("Eletrônicos");
if (categoria != null)
{
    Console.WriteLine($"Categoria encontrada: {categoria.Nome}");
}
else
{
    Console.WriteLine("Categoria não encontrada");
}
Criar Novo Produto
Para criar um novo produto:

csharp
Copiar código
Produto produtoRepo = new Produto("string_conexao");
Models.Produto novoProduto = new Models.Produto
{
    Nome = "Smartphone",
    Descricao = "Smartphone de última geração",
    Preco = 1500.00m,
    QuantidadeEstoque = 50,
    CategoriaId = 1
};
bool sucesso = produtoRepo.CriarProduto(novoProduto);
Buscar Produto por ID
Para buscar um produto pelo ID:

csharp
Copiar código
Produto produtoRepo = new Produto("string_conexao");
Models.Produto produto = produtoRepo.SelectById(1);
if (produto != null)
{
    Console.WriteLine($"Produto encontrado: {produto.Nome}");
}
else
{
    Console.WriteLine("Produto não encontrado");
}
Realizar Login de Usuário
Para realizar login de um usuário:

csharp
Copiar código
User userRepo = new User("string_conexao");
Models.Usuarios usuario = userRepo.Select("admin", "senha123");
if (usuario != null)
{
    Console.WriteLine($"Usuário {usuario.Nome} logado com sucesso");
}
else
{
    Console.WriteLine("Credenciais inválidas");
}
Métodos Disponíveis
CategoriaRepository
SelectByName(string nome): Busca uma categoria pelo nome.
CriarCategoria(Models.Categoria categoria): Cria uma nova categoria.
ProdutoRepository
SelectAll(): Retorna todos os produtos cadastrados.
SelectById(int id): Retorna um produto específico pelo seu ID.
SelectByStatus(string status): Retorna produtos com o status especificado.
CriarProduto(Models.Produto produto): Cria um novo produto.
UpdateProdutos(Models.Produto produto): Atualiza um produto existente.
DeleteById(int id): Deleta um produto pelo seu ID.
UserRepository
SelectAll(): Retorna todos os usuários cadastrados.
Select(string nome, string senha): Realiza login de um usuário.
CreateUser(Models.Usuarios user): Cria um novo usuário.

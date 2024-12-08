create database GerenciadorProdutos
use GerenciadorProdutos

CREATE TABLE Categorias (
    Id INT IDENTITY PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL
);

CREATE TABLE Produtos (
    Id INT IDENTITY PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(255),
    Status NVARCHAR(50),
    Preco DECIMAL(18, 2) NOT NULL,
    QuantidadeEstoque INT NOT NULL,
   CategoriaId INT NOT NULL FOREIGN KEY REFERENCES Categorias(Id),
   CategoriaNome NVARCHAR(100)
);
CREATE TABLE Usuarios (
    Id INT IDENTITY PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Senha NVARCHAR(100) NOT NULL,
    Papel NVARCHAR(50) NOT NULL
);

SELECT 
    p.Id,
    p.Nome,
    p.Descricao,
    p.Preco,
    p.QuantidadeEstoque,
    p.Status,
    c.Nome AS CategoriaNome
FROM Produtos p
INNER JOIN Categorias c ON p.CategoriaId = c.Id;

CREATE TRIGGER trg_AtualizarCategoriaNome
ON Produtos
AFTER INSERT
AS
BEGIN
    
    UPDATE p
    SET p.CategoriaNome = c.Nome
    FROM Produtos p
    INNER JOIN Categorias c ON p.CategoriaId = c.Id
    WHERE p.Id IN (SELECT Id FROM INSERTED);
END;


CREATE VIEW ProdutosView AS
SELECT 
    Id, 
    Nome, 
    Descricao, 
    Preco, 
    QuantidadeEstoque, 
    CASE 
        WHEN QuantidadeEstoque > 0 THEN 'Em estoque'
        ELSE 'Indisponível'
    END AS Status,
    CategoriaNome
FROM Produtos;

CREATE TRIGGER trg_AtualizarStatus
ON Produtos
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE p
    SET p.Status = CASE
        WHEN p.QuantidadeEstoque > 0 THEN 'Em estoque'
        ELSE 'Indisponível'
    END
    FROM Produtos p
    WHERE p.Id IN (SELECT Id FROM INSERTED);
END;








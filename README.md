# 🛡️ ApiAutenticacao

API RESTful em ASP.NET Core com autenticação via JWT, controle de usuários, gerenciamento de produtos, carrinho de compras e pedidos.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT (Json Web Token)
- Autenticação via Windows
- Injeção de Dependência
- Boas práticas (SOLID, Clean Code)

---

## 📦 Funcionalidades

### 🔐 Autenticação
- Registro de usuários
- Login com geração de token JWT
- Proteção de rotas via `[Authorize]`

### 🛍️ Produtos
- Cadastro, listagem, atualização e remoção de produtos (CRUD)

### 🛒 Carrinho
- Adicionar produtos ao carrinho
- Listar itens do carrinho
- Atualizar quantidade
- Remover item
- Limpar carrinho

### 📦 Pedidos
- Finalizar compra com base no carrinho
- Geração automática de pedido e seus itens
- Armazena o histórico de pedidos por usuário

---

## 🧪 Como rodar o projeto localmente

### Pré-requisitos

- [.NET SDK 6.0+](https://dotnet.microsoft.com/en-us/download)
- SQL Server (local ou em container)
- Git

### Passos

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/ApiAutenticacao.git
cd ApiAutenticacao

# Restaure os pacotes
dotnet restore

# Aplique as migrations no banco de dados
dotnet ef database update

# Rode o projeto
dotnet run

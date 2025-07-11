
# 📚 LivrariaApi

API REST desenvolvida com ASP.NET Core para gerenciamento de livros e autores, utilizando arquitetura em camadas e boas práticas de desenvolvimento.

---

## 🚀 Tecnologias Utilizadas

- [.NET 6](https://dotnet.microsoft.com/download)
- ASP.NET Core Web API
- **FluentValidation** – para validações robustas
- **AutoMapper** – para mapeamento entre entidades e DTOs
- **DbConnection** com `IDbTransaction` – para controle transacional explícito
- **Dependency Injection** nativa do .NET
- **RESTful APIs** com controllers separados por domínio

---

## 🔧 Requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- Um banco de dados (ex: SQL Server, PostgreSQL)
- Ferramenta para testar a API (Postman, Insomnia, etc.)

---

## 📦 Estrutura do Projeto

```
/LivrariaApi.sln
│
├─ Application
│   ├─ DTOs (Livros, Autores)
│   ├─ Enums
│   ├─ Models (para retorno de operação)
│   ├─ Notification (padronização de notificações/erros)
│   ├─ Validators (FluentValidation)
│
├─ Domain
│   ├─ Entidades (Livro.cs, Autor.cs)
│   ├─ Enums (GeneroEnum.cs)
│
├─ Infrastructure
│   ├─ Interfaces (contratos dos repositórios)
│   ├─ Repositories (implementações com transações via DbConnection)
│
└─ LivrariaApi
    ├─ Controllers (AutorController, LivroController)
    ├─ Program.cs (startup principal)
    ├─ appsettings.json (configuração)
    └─ LivrariaApi.http (testes de endpoints)
```

---

## 🧪 Como executar

1. Clone o repositório:

```bash
git clone https://github.com/GabrieldSantana/LivrariaApi.git
cd LivrariaApi
```

2. Restaure dependências:

```bash
dotnet restore
```

3. Rode a aplicação:

```bash
dotnet run --project LivrariaApi
```

A API estará disponível em: `https://localhost:5001`

---

## 📄 Endpoints Disponíveis

### 📘 Livros

| Método | Rota               | Descrição                  |
|--------|--------------------|----------------------------|
| GET    | `/api/livro`       | Lista todos os livros      |
| GET    | `/api/livro/{id}`  | Detalha um livro específico |
| POST   | `/api/livro`       | Cadastra um novo livro     |

### 👨‍💼 Autores

| Método | Rota               | Descrição                  |
|--------|--------------------|----------------------------|
| GET    | `/api/autor`       | Lista todos os autores     |
| GET    | `/api/autor/{id}`  | Detalha um autor específico |
| POST   | `/api/autor`       | Cadastra um novo autor     |

---

## ✅ Boas Práticas Aplicadas

- Separação por camadas (Domain, Application, Infrastructure, API)
- Validação com FluentValidation desacoplada da controller
- Repository Pattern para acesso a dados
- Uso de AutoMapper para transformar entidades em DTOs
- Transações explícitas com `IDbTransaction` para controle de consistência
- Notificações desacopladas para mensagens de erro

---

## 🛠 Próximas Melhorias

- Adicionar autenticação (ex: JWT)
- Cobertura de testes (unitários e integração)

---

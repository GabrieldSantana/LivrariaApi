
# 📚 LivrariaApi

API REST desenvolvida com ASP.NET Core para gerenciamento de livros e autores, utilizando arquitetura em camadas e boas práticas de desenvolvimento.

---

## 🚀 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/download)
- ASP.NET Core Web API
- **FluentValidation** – para validações robustas
- **AutoMapper** – para mapeamento entre entidades e DTOs
- **DbConnection** com `IDbTransaction` – para controle transacional explícito
- **Dependency Injection** nativa do .NET
- **RESTful APIs** com controllers separados por domínio

---

## 🔧 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Banco de dados SQL Server

---

## 📦 Estrutura do Projeto

```
/LivrariaApi.sln
│
├─ Application
│   ├─ Interfaces (para retorno de operação)
│   │   ├─ IMainService
│   │   │   └─ IMainService.cs
│   │   ├─ IAutorSevice.cs
│   │   └─ ILivroSevice.cs
│   ├─ Notification (padronização de notificações/erros)
│   ├─ Validators (FluentValidation)
│   ├─ Services
│   │   ├─ MainService
│   │   │   └─ MainService.cs
│   │   ├─ AutorService.cs
│   └─  └─ LivroService.cs
│
├─ Config
│   ├─ AutoMapper.cs
│   └─ DependencyInjection.cs
├─ Controllers
│   ├─ MainController
│   │   └─ MainController.cs
│   ├─ AutorController.cs
│   └─ LivroController.cs
├─ db
│   ├─ Create for LIVRARIA_DB.sql
│   ├─ Insert for LIVRARIA_DB.sql
│   └─ Logger Table and Triggers for LIVRARIA_DB.sql (Ainda não implementado)
├─ Domain
│   ├─ DTOs
│   │   ├─ AutorDto.cs
│   │   └─ LivroDto.cs
│   ├─ Enums
│   │   └─ GeneroEnum.cs
│   ├─ Models
│   │   ├─ AutorModel.cs
│   │   ├─ LivroModel.cs
│   │   └─ RetornoPaginado.cs
│   ├─ Notification
│   │   └─ Notificacao.cs
│   ├─ Validators
│   │   ├─ AutorValidator.cs
│   └─  └─ LivroValidator.cs
│
├─ Infrastructure
│   ├─ Interfaces (contratos dos repositórios)
│   │   ├─ IAutorRepository.cs
│   │   └─ ILivroRepository.cs
│   ├─ Repositories (implementações com transações via DbConnection)
│   │   ├─ AutorRepository.cs
│   └─  └─ LivroRepository.cs
│
├─ LivrariaApi
│   ├─ Controllers (AutorController, LivroController)
│   ├─ Program.cs (startup principal)
│   ├─ appsettings.json (configuração)
│   └─ LivrariaApi.http (testes de endpoints)
├─ appsettings.json
├─ Program.cs
└─ README.md
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
| GET    | `/api/livro/{pagina}/{qtdPagina}`       | Lista paginada dos livros  |
| GET    | `/api/livro/{id}`  | Detalha um livro específico |
| POST   | `/api/livro`       | Cadastra um novo livro     |
| PUT   | `/api/livro/{id}`       | Atualizar um livro específico     |
| DELETE | `/api/livro/{id}`  | Remove um livro específico |

### 👨‍💼 Autores

| Método | Rota               | Descrição                  |
|--------|--------------------|----------------------------|
| GET    | `/api/autor`       | Lista todos os autores     |
| GET    | `/api/autor/{pagina}/{qtdPagina}`       | Lista paginada dos autores  |
| GET    | `/api/autor/{id}`  | Detalha um autor específico |
| POST   | `/api/autor`       | Cadastra um novo autor     |
| PUT   | `/api/autor/{id}`       | Atualizar um autor específico     |
| DELETE | `/api/autor/{id}`  | Remove um autor específico |

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

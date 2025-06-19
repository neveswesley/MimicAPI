# 🎭 Projeto: Mimic API

API RESTful desenvolvida para um **jogo de mímica**, com sistema de pontuação, controle de palavras (ativação/desativação) e sincronização com banco de dados local no aplicativo mobile.

Este projeto tem como foco a organização e persistência dos dados do jogo, permitindo que o app consuma os dados de forma rápida, segura e estruturada.

---

## 🚀 Funcionalidades

- 🎮 Cadastro e gerenciamento de palavras do jogo
- ✅ Ativação e desativação de palavras
- 🧠 Sistema de pontuação por rodada
- 🔄 Sincronização com banco de dados local do app mobile
- 🔍 Consultas por categoria, dificuldade ou estado da palavra
- 🧪 Testes simples de rotas via Postman

---

## 🛠️ Tecnologias Utilizadas

- **ASP.NET Web API**
- **C#**
- **Entity Framework Core**
- **SQL Server**
- **Postman** (testes de endpoints)

---

## 📦 Estrutura do Projeto
MimicAPI/

├── Controllers/ # Endpoints HTTP para gerenciamento do jogo

├── Models/ # Entidades do jogo (Palavra, Pontuação, etc.)

├── Repositories/ # Camada de acesso e manipulação de dados

├── Database/ # Contexto do Entity Framework Core

├── Program.cs # Inicialização e configuração da API

└── appsettings.json # Configurações da aplicação e conexão com o banco


---

## 📮 Principais Endpoints

| Verbo  | Rota                     | Ação                              |
|--------|--------------------------|-----------------------------------|
| GET    | /api/palavras            | Listar todas as palavras          |
| GET    | /api/getbyid             | Buscar uma palavra pelo ID        |
| POST   | /api/palavras            | Cadastrar nova palavra            |
| PUT    | /api/palavras/{id}       | Atualizar palavra                 |
| PATCH  | /api/palavras/{id}/ativo | Ativar ou desativar palavra       |

---

## 🧪 Testes

Testes simples foram realizados com **Postman**, permitindo verificar se os endpoints estão funcionando corretamente com os parâmetros esperados.

---

## 📂 Banco de Dados

- Utilizado **SQL Server**
- Modelagem relacional com chave primária, relacionamentos e atributos controlados via EF Core

---

## 🤝 Contato

Desenvolvido por **Wesley Luan das Neves Miranda**

- [LinkedIn](https://linkedin.com/in/neveswesley)
- Email: wellynvs@gmail.com

---

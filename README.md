<div align="center">

# 🍺 PingDevBar

### Sistema de gerenciamento de bar desenvolvido em **ASP.NET MVC**
Controle de donos, mesas, garçons, produtos, contas e pedidos com separação de responsabilidades, arquitetura em camadas e **foco em testes automatizados**.

---

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET MVC](https://img.shields.io/badge/ASP.NET-MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Testes](https://img.shields.io/badge/Testes-Automatizados-brightgreen?style=for-the-badge&logo=checkmarx&logoColor=white)
![Status](https://img.shields.io/badge/Status-Concluído-brightgreen?style=for-the-badge)

</div>

---

# 📌 Sobre o Projeto

O **Controle de Bar** foi desenvolvido com foco em:

- Arquitetura limpa em camadas (Domain, Application, Infra, WebApp)
- Separação de responsabilidades
- Regras de negócio reais
- Encapsulamento
- Comunicação entre objetos
- Deploy na Azure
- **Testes automatizados cobrindo as principais regras de negócio**


A aplicação permite que donos de estabelecimentos gerenciem mesas, garçons, produtos, contas e pedidos de forma prática, com cálculo automático do total das contas e do faturamento diário do bar.



---

## 🌐 Acesse no Navegador

A aplicação está publicada e disponível online — sem necessidade de instalação:

**👉 [Acessar PingDevBar](https://pingdevbar-fvbdceatcmb7f7eb.canadacentral-01.azurewebsites.net/)**

---


## 🎬 Demonstração

![Demonstração do Sistema](./demo.gif)

---

# ✅ Funcionalidades

## 👤 Donos

- Cadastro de donos
- Visualização, edição e exclusão
- Associação com estabelecimento

### Regras

- Nome obrigatório (2–100 caracteres)
- E-mail obrigatório e válido
- Senha obrigatória
- Estabelecimento obrigatório

---

## 🪑 Mesas

- Cadastro de mesas
- Visualização, edição e exclusão
- Controle de status em tempo real

### Regras

- Número da mesa obrigatório
- Quantidade de lugares obrigatória
- Status obrigatório: `Livre` ou `Ocupada`
- Associação com estabelecimento obrigatória

---

## 🧑‍🍽️ Garçons

- Cadastro de garçons
- Visualização, edição e exclusão
- Associação com estabelecimento

### Regras

- Nome obrigatório (2–100 caracteres)
- Associação com estabelecimento obrigatória

---

## 🍔 Produtos

- Cadastro de produtos
- Visualização, edição e exclusão
- Associação com estabelecimento

### Regras

- Nome obrigatório (2–100 caracteres)
- Preço obrigatório
- Associação com estabelecimento obrigatória

---

## 🧾 Contas

- Abertura de contas
- Visualização, edição e encerramento
- Visualização dos pedidos vinculados
- Cálculo automático do valor total da conta

### Regras

- Garçom obrigatório
- Nome do cliente obrigatório (2–100 caracteres)
- Data de abertura gerada automaticamente
- Situação: `Aberta` ou `Fechada`
- Mesa obrigatória
- Associação com estabelecimento obrigatória

---

## 📋 Pedidos

- Registro de pedidos vinculados a uma conta
- Visualização e remoção de pedidos
- Cálculo automático do valor total da conta
- Cálculo automático do faturamento diário do bar

### Regras

- Produto obrigatório
- Quantidade obrigatória
- Conta obrigatória
- Total calculado automaticamente com base no produto e na quantidade

---

# 🧪 Testes Automatizados

O projeto possui **três camadas de testes automatizados**, cobrindo domínio, aplicação e integração, garantindo a confiabilidade das regras de negócio.

| Projeto de Testes | Foco |
|---|---|
| `Controle-De-Bar.Tests` (Domínio) | Validações das entidades e regras de negócio |
| `Controle-De-Bar.Tests` (Aplicação) | Comportamento dos serviços e casos de uso |
| `Controle-De-Bar.Tests` (Integração) | Fluxos completos entre camadas |

Casos cobertos pelos testes:

- ✔️ Cálculo do valor total dos pedidos
- ✔️ Cálculo do faturamento diário
- ✔️ Validações obrigatórias de campos
- ✔️ Regras de status de mesas e contas
- ✔️ Integridade entre pedidos e contas

---


# 📂 Estrutura do Projeto

```bash
📦 controle-de-bar
 ┣ 📁 src
 ┃ ┣ 📁 ControleDeBar.Dominio
 ┃ ┃ ┣ 📁 ModuloDono
 ┃ ┃ ┣ 📁 ModuloMesa
 ┃ ┃ ┣ 📁 ModuloGarcom
 ┃ ┃ ┣ 📁 ModuloProduto
 ┃ ┃ ┣ 📁 ModuloConta
 ┃ ┃ ┗ 📁 ModuloPedido
 ┃ ┣ 📁 ControleDeBar.Aplicacao
 ┃ ┃ ┣ 📁 ModuloDono
 ┃ ┃ ┣ 📁 ModuloMesa
 ┃ ┃ ┣ 📁 ModuloGarcom
 ┃ ┃ ┣ 📁 ModuloProduto
 ┃ ┃ ┣ 📁 ModuloConta
 ┃ ┃ ┗ 📁 ModuloPedido
 ┃ ┣ 📁 ControleDeBar.Infra
 ┃ ┃ ┣ 📁 Compartilhado
 ┃ ┃ ┗ 📁 Migrations
 ┃ ┗ 📁 ControleDeBar.WebApp
 ┃   ┣ 📁 Controllers
 ┃   ┣ 📁 Views
 ┃   ┣ 📁 wwwroot
 ┃   ┗ 📜 Program.cs
 ┣ 📁 tests
 ┃ ┣ 📁 Controle-De-Bar.Tests (Domínio)
 ┃ ┣ 📁 Controle-De-Bar.Tests (Aplicação)
 ┃ ┗ 📁 Controle-De-Bar.Tests (Integração)
 ┗ 📜 README.md
```

---

# ⚙️ Tecnologias Utilizadas

- C#
- ASP.NET MVC
- Bootstrap
- SQL Server
- Entity Framework
- Azure

---

# ▶️ Como Executar

## 1. Clone o repositório

```bash
git clone https://github.com/KauanGalvani/controle-de-bar.git
```

## 2. Acesse a pasta do projeto

```bash
cd controle-de-bar
```

## 3. Configure a string de conexão

No arquivo `appsettings.json`, configure a conexão com o SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=ControleDeBar;Trusted_Connection=True;"
}
```

## 4. Execute as migrations

```bash
dotnet ef database update
```

## 5. Execute o projeto

```bash
dotnet run --project src/ControleDeBar.WebApp
```

## 6. Acesse no navegador

```
https://localhost:5001
```

## 7. Execute os testes

```bash
dotnet test
```

---

# 📋 Requisitos

- .NET SDK instalado
- SQL Server instalado
- Visual Studio 2022 ou superior

---

# 🎯 Objetivo de Aprendizado

Este projeto foi desenvolvido para praticar:

- ✔️ Arquitetura em camadas (Domain, Application, Infra, WebApp)
- ✔️ Desenvolvimento de aplicações web com ASP.NET
- ✔️ Estilização responsiva com Bootstrap e React
- ✔️ Persistência de dados com SQL Server e Entity Framework
- ✔️ Testes automatizados em múltiplas camadas
- ✔️ Modelagem de entidades reais
- ✔️ Aplicação de regras de negócio
- ✔️ Organização limpa e reutilizável do código

---

## 👨‍💻 Autores

<div align="center">

Desenvolvido por **Kauan Galvani** e **Kauan Silva** como parte dos estudos em **Testes Automatizados na Academia Do Programador**.

[![GitHub](https://img.shields.io/badge/GitHub-KauanGalvani-181717?style=for-the-badge&logo=github)](https://github.com/KauanGalvani)

[![GitHub](https://img.shields.io/badge/GitHub-k--silvax19-181717?style=for-the-badge&logo=github)](https://github.com/k-silvax19)

</div>
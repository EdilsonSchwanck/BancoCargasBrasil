# Cargas Brasil - API

Projeto feito para um trabalho da faculdade.

É uma API em C# para cadastro de motoristas de caminhão. O motorista faz o cadastro com os dados dele, do veículo e manda as fotos da CNH e do documento do veículo. Depois alguém do backoffice entra, vê os cadastros pendentes e aprova ou não.

## O que o projeto tem

- Cadastro de motorista
- Login do motorista (retorna um token JWT)
- Exclusão da conta pelo próprio motorista
- Login do administrador
- Listagem dos cadastros que ainda não foram aprovados
- Aprovação do cadastro

Feito com .NET 10, Entity Framework e MySQL. Tem Swagger para testar as rotas.

## Rotas

```
POST   /api/CadastrarMotorista    cadastro (aberto)
POST   /api/Login                 login do motorista (aberto)
DELETE /api/ExcluirUsuario        exclui a conta (precisa estar logado como motorista)
POST   /api/admin/Login           login do admin (aberto)
GET    /api/ConsultarFormulario   lista cadastros pendentes (só admin)
POST   /api/AprovarCadastro       aprova um cadastro (só admin)
```

Nas rotas que precisam de login tem que mandar o token no header: `Authorization: Bearer <token>`

## O que foi feito na última versão

A primeira versão funcionava mas tinha bastante problema, principalmente de segurança. Nessa versão:

- Tirei a senha do banco e a chave do JWT do código. Agora ficam no user-secrets (ou em variável de ambiente no servidor).
- A senha do usuário era salva com SHA1 sem salt. Troquei para PBKDF2 com salt. Quem já tinha cadastro continua conseguindo entrar, e a senha é atualizada para o formato novo no próximo login.
- Tinha uma rota que gerava token só com o CPF, sem senha. Removi, o token agora só sai no login.
- As rotas do backoffice estavam abertas para qualquer um. Agora só o admin acessa.
- Para excluir a conta precisa estar logado e confirmar a senha.
- Coloquei limite de tentativas no login e no cadastro.
- Validação dos campos do cadastro, incluindo se o CPF é válido.
- Não deixa cadastrar o mesmo CPF duas vezes.
- Atualizei de .NET 7 para .NET 10 (o 7 não tem mais suporte).

Também reorganizei o código em camadas, seguindo MVC e SOLID:

```
Controllers   recebem as requisições
Services      regras de negócio
Repositories  acesso ao banco
Models        entidades e DTOs
Security      hash de senha e geração do token
Data          contexto do banco e mapeamento da tabela
```

Antes estava tudo dentro do controller. Agora cada parte faz uma coisa só, e as classes recebem as dependências por interface (injeção de dependência).

## Como rodar

Precisa ter instalado:
- .NET 10 SDK
- MySQL
- dotnet-ef (`dotnet tool install --global dotnet-ef`)

Entra na pasta do projeto:

```
cd CargasBrasilDB/CargasBrasilDB
```

Configura a conexão com o banco e a chave do JWT (a chave precisa ter pelo menos 32 caracteres):

```
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=CargasBrasilDB2;Uid=root;Pwd=SUA_SENHA"
dotnet user-secrets set "Jwt:Key" "coloque-aqui-uma-chave-grande-com-32-ou-mais-caracteres"
```

Para usar o backoffice, cria o admin. O primeiro comando gera o hash da senha, aí copia o resultado e coloca no terceiro:

```
dotnet run -- hash-password "senhaDoAdmin"
dotnet user-secrets set "Admin:Usuario" "admin"
dotnet user-secrets set "Admin:SenhaHash" "HASH_GERADO"
```

Cria as tabelas e roda:

```
dotnet ef database update
dotnet run
```

O Swagger fica em https://localhost:7059/swagger



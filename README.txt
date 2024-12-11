PROJETO: Conversor de Logs - iTaaS Solution

Este projeto implementa uma API REST em C# para realizar a conversão de arquivos de log do formato utilizado pela "MINHA CDN" para o formato desejado pelo sistema "Agora". A API também suporta diversas operações relacionadas ao gerenciamento e transformação de logs.

DESCRIÇÃO DO PROBLEMA:
A iTaaS Solution enfrenta altos custos operacionais com serviços de CDN. Após contratar a "MINHA CDN", foi identificado que o formato de log fornecido por ela não é compatível com o sistema de faturamento "Agora". Este projeto resolve esse problema criando um sistema capaz de transformar e gerenciar os logs fornecidos.

EXEMPLO DE CONVERSÃO:
Formato de entrada ("MINHA CDN"):
312|200|HIT|"GET /robots.txt HTTP/1.1"|100.2
101|200|MISS|"POST /myImages HTTP/1.1"|319.4
199|404|MISS|"GET /not-found HTTP/1.1"|142.9
312|200|INVALIDATE|"GET /robots.txt HTTP/1.1"|245.1

Formato de saída ("Agora"):
#Version: 1.0
#Date: 15/12/2017 23:01:06
#Fields: provider http-method status-code uri-path time-taken response-size cache-status
"MINHA CDN" GET 200 /robots.txt 100 312 HIT
"MINHA CDN" POST 200 /myImages 319 101 MISS
"MINHA CDN" GET 404 /not-found 143 199 MISS
"MINHA CDN" GET 200 /robots.txt 245 312 REFRESH_HIT

ENDPOINTS IMPLEMENTADOS:

Transformação de um formato de log para outro
POST /api/logs/TransformarLog

Transforma logs enviados diretamente ou identificados por ID.

Formato de saída pode ser salvo em arquivo ou retornado como JSON.

POST /api/logs/TransformarDeUrl

Obtém logs de uma URL, transforma e salva/retorna o resultado.

Buscar Logs Salvos
GET /api/logs/BuscarLogsSalvos

Retorna todos os logs salvos no banco de dados.

Buscar Logs Transformados no Backend
GET /api/logs/ObterPorId?id={id}

Retorna o log original e transformado pelo identificador.

Buscar Logs Salvos por Identificador
GET /api/logs/ObterPorId?id={id}

Similar ao endpoint acima, mas destaca o formato original.

Salvar Logs
POST /api/logs/SalvarLog

Salva o log original no banco de dados para transformações futuras.

Remover Log
DELETE /api/logs/RemoverLog?id={id}

Remove um log salvo do banco de dados.

ESTRUTURA DO PROJETO:
O projeto segue a arquitetura DDD (Domain-Driven Design) com as seguintes camadas:

API: Camada de apresentação, responsável por expor os endpoints REST.

Application: Contém os serviços de aplicação e lógica de negócios.

Domain: Define as entidades e interfaces do domínio.

Infra: Implementação de repositórios e interação com o banco de dados.

TECNOLOGIAS UTILIZADAS:

C# com .NET Core 2.1

Entity Framework Core para interação com o banco de dados SQL Server

Visual Studio 2022 como IDE de desenvolvimento

SQL Server para persistência de dados

HttpClient para requisições externas

COMO CONFIGURAR O PROJETO:

Configurar o banco de dados:

Atualize a string de conexão no arquivo appsettings.json:
"ConnectionStrings": {
"DefaultConnection": "Server=localhost;Database=ConverterLog;User Id=sa;Password=sua-senha;"
}

Executar o projeto:

Abra o projeto no Visual Studio 2022.

Defina o projeto como "Startup Project".

Pressione F5 para iniciar o servidor.

Testar os Endpoints:

Use ferramentas como Postman ou cURL para interagir com a API.

TESTES E QUALIDADE DE CÓDIGO:

Testes unitários foram implementados para validar as principais funcionalidades.

O código segue princípios de SOLID e Clean Architecture, garantindo extensibilidade e manutenção.

AUTOR: Kauã Alves Silva 
Para iTaaS Solution
ANO: 2024
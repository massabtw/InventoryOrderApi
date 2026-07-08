# InventoryOrderAPI

API simples para gerenciamento de pedidos (Orders) construída com ASP.NET Core (.NET 10) e Dapper.

Resumo rápido
- Projeto: InventoryOrderAPI
- Stack: .NET 10, ASP.NET Core Web API, Dapper (micro-ORM), Microsoft.Data.SqlClient
- Objetivo: Demonstrar um fluxo CRUD de pedidos com padronização de erros via ErrorModel

Principais responsabilidades
- Controllers: rotas HTTP (/api/Order, /api/TestErrors)
- Services: validações de negócio e orquestração (OrderService)
- Repositório: acesso ao banco com Dapper (OrderRepository)
- Helpers: ErrorMessages para centralizar ErrorModel (Code / Message / MessageCustomer)

Requisitos
- .NET SDK 10 (net10.0)
- SQL Server acessível (connection string configurada em appsettings.json)
- (Opcional) Postman ou newman para rodar coleções

Como rodar localmente
1. Restaurar e compilar

```powershell
dotnet restore
dotnet build
```

2. Rodar a API (exemplo via CLI)

```powershell
dotnet run --project .\InventoryOrderAPI\InventoryOrderAPI.csproj
```

Ao rodar, veja no console as URLs em que a aplicação está escutando (launchSettings indica por padrão: https://localhost:7041 e http://localhost:5220).

Se você usar HTTPS e tiver problemas no Postman, desative a verificação de certificado (Settings → SSL certificate verification = OFF) ou chame via HTTP se disponível.

Configuração de banco
- A classe DbConnectionFactory lê a connection string nomeada `DefaultConnection` nas configurações. Verifique `appsettings.json` ou suas variáveis de ambiente.
- Se a connection string faltar, a aplicação lança InvalidOperationException no startup.

Endpoints principais

OrderController (rota base: /api/Order)
- POST /api/Order
  - Cria um pedido. Body JSON: { productId, quantity, productPrice }
  - Valida: productId >= 0, quantity > 0, productPrice > 0

- GET /api/Order
  - Lista todos os pedidos

- GET /api/Order/{id}
  - Retorna pedido por id

- PUT /api/Order/{id}
  - Atualiza quantidade / preço e recalcula TotalValue

- DELETE /api/Order/{id}
  - Deleta o pedido (retorna 204 No Content em sucesso)

TestErrorsController (para testes de mensagens de erro)
- GET /api/TestErrors/OrderNotFound/{id}           -> 404 com ErrorModel
- GET /api/TestErrors/ProductNotFound/{id}         -> 404 com ErrorModel
- GET /api/TestErrors/InvalidQuantity/{quantity}   -> 400 com ErrorModel
- GET /api/TestErrors/InvalidPrice/{price}         -> 400 com ErrorModel
- GET /api/TestErrors/UnknownError?message=texto    -> 500 com ErrorModel

Sobre ErrorModel
- Tipo fornecido pelo pacote GrupoMadero.Common.Results. Campos usados:
  - Code: código interno do erro (ex.: "OrderNotFound")
  - Message: mensagem técnica (logs / diagnóstico)
  - MessageCustomer: mensagem amigável para exibir ao cliente

Padrão de tratamento
- Services retornam Result<T> / Result (do pacote) contendo Value ou Errors
- Extensões em `InventoryOrderAPI/Extensions/ResultExtension.cs` mapeiam Result → IActionResult (Ok, Created, NoContent, BadRequest)

Testes com Postman
- Coleção (importar): `postman/InventoryOrderAPI.postman_collection.json`
- Variável a ajustar: `baseUrl` (ex.: https://localhost:7041)
- A coleção inclui cenários: create (válido/inválido), get all, get not found, delete, delete not found.

Exemplos curl
- Criar pedido
```bash
curl -k -X POST "https://localhost:7041/api/Order" -H "Content-Type: application/json" -d '{"productId":1,"quantity":2,"productPrice":10.5}'
```

- Deletar pedido
```bash
curl -k -X DELETE "https://localhost:7041/api/Order/1"
```

- Testar mensagem de erro (simulada)
```bash
curl -k "https://localhost:7041/api/TestErrors/OrderNotFound/9999"
```

Observações e troubleshooting
- Se o build falhar por causa de pacotes privados (feed NuGet internal), verifique suas credenciais de feed ou remova referências temporariamente.
- As portas padrão estão em `InventoryOrderAPI/Properties/launchSettings.json` (https://localhost:7041, http://localhost:5220).
- Em ambientes CI, centralizar mapeamento ErrorModel → status code via middleware é recomendado.

Próximos passos sugeridos
- Adicionar testes unitários e de integração (mock do repositório para Service tests).
- Centralizar mapeamento de erros (middleware) e internacionalização das mensagens para MessageCustomer.
- Adicionar autenticação/autorização e policies para endpoints sensíveis.

Contribuição
- Fork → branch feature/xxx → PR com descrição clara das mudanças.

Licença
- Repositório exemplo — verifique com o time qual licença aplicar.

Contato
- Para dúvidas, abra issue no repositório ou fale com o responsável técnico.

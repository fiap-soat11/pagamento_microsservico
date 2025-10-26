# Configurações de Dados

Como proceder para realizar a migration do Modelo gerado para tabelas no Banco de dados

Abrir algum terminal e navagar até a pasta fisica:

DataSource

## Criação dos mapeamentos

### Passos:
#### - Garanta que o ef esteja instalado  

```console
dotnet tool install --global dotnet-ef
```

#### - Gerar mapeamento
```console
dotnet ef dbcontext scaffold --verbose --no-onconfiguring "Server=localhost;Port=3336;Database=fiap;Uid=user_fiap;Pwd=pass_fiap;" Pomelo.EntityFrameworkCore.MySql --context-dir ".\Context" -o "..\Domain\Entities" -f --no-build -c ApplicationDbContext -s ".\"    
```

## Geração de script para criar base de dados 
#### - Apagar todas migrations e ApplicationDbContextModelSnapshot
#### - Criar migration (exemplo migration inicial)
```console
dotnet ef migrations add 'migration inicial'
```

#### - Deletar o setup.sql da pasta raiz
```console
dotnet ef migrations script --idempotent --output "../setup.sql" --context ApplicationDbContext
```

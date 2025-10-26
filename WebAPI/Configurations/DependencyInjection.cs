using Adapters.Controllers;
using Adapters.Controllers.Interfaces;
using Adapters.Gateways;
using Adapters.Gateways.Interfaces;
using Adapters.Validators;
using Application.Interfaces;
using Application.UseCases;
using DataSource.Context;
using DataSource.Repositories;
using DataSource.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace WebAPI.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructure(this IServiceCollection Services, IConfiguration configuration)
        {

            #region conexões
            var mySqlConnectionString = configuration.GetConnectionString("DefaultConnection");
            /* serviços de banco de dados MySql  */
            
            Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(mySqlConnectionString,ServerVersion.AutoDetect(mySqlConnectionString))
               .UseLoggerFactory(
                   LoggerFactory.Create(
                       b => b
                           .AddConsole()
                           .AddFilter(level => level >= LogLevel.Information)))
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
            });

            #endregion


           

            /* ***** serviços de acesso a base ***** */
            Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(mySqlConnectionString, ServerVersion.AutoDetect(mySqlConnectionString)));
            Services.AddScoped<IDataSource, DataSource.DataSource>();


            /* ***** serviços de orquestração ***** */
            Services.AddScoped<IClienteController, ClienteController>();
            Services.AddScoped<IQRCodeController, QRCodeController>();
            Services.AddScoped<ICategoriaController, CategoriaController>();
            Services.AddScoped<IProdutoController, ProdutoController>();
            Services.AddScoped<IPedidoController, PedidoController>();
            Services.AddScoped<IPagamentoController, PagamentoController>();

            /* ***** serviços de acesso a dados ***** */
            Services.AddScoped<IClienteGateway, ClienteGateway>();
            Services.AddScoped<IPedidoGateway, PedidoGateway>();
            Services.AddScoped<ICategoriaGateway, CategoriaGateway>();
            Services.AddScoped<IProdutoGateway, ProdutoGateway>();
            Services.AddScoped<IStatusGateway, StatusGateway>();


            /* ***** serviços de negocio ***** */
            Services.AddScoped<IClienteUseCase, ClienteUseCase>();
            Services.AddScoped<ICategoriaUseCase, CategoriaUseCase>();
            Services.AddScoped<IProdutoUseCase, ProdutoUseCase>();
            Services.AddScoped<IMercadoPagoUseCase, MercadoPagoUseCase>();
            Services.AddScoped<IPagamentoUseCase, PagamentoUseCase>();
            Services.AddScoped<IPedidoUseCase, PedidoUseCase>();

            Services.AddScoped<IClienteRepository, ClienteRepository>();          
            Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            Services.AddScoped<IPedidoProdutoRepository, PedidoProdutoRepository>();
            Services.AddScoped<IPedidoRepository, PedidoRepository>();
            Services.AddScoped<IStatusRepository, StatusRepository>();
            Services.AddScoped<IProdutoRepository, ProdutoRepository>();
           
            return Services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddFluentValidationAutoValidation();
            Services.AddValidatorsFromAssemblyContaining<ClienteRequestValidator>();
            
            return Services;
        }



    }


}
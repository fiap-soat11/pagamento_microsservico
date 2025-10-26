using Adapters.Controllers;
using Adapters.Controllers.Interfaces;
using Adapters.Gateways;
using Adapters.Gateways.Interfaces;
using Application.Interfaces;
using Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection Services, IConfiguration configuration)
        {
            return Services;
            
            /* ***** serviços de orquestração ***** */
            /*
            Services.AddScoped<IClienteController, ClienteController>();
            Services.AddScoped<IQRCodeController, QRCodeController>();
            Services.AddScoped<ICategoriaController, CategoriaController>();
            Services.AddScoped<IProdutoController, ProdutoController>();
            Services.AddScoped<IPedidoController, PedidoController>();
            */

            /* ***** serviços de acesso a dados ***** */
            /*
            Services.AddScoped<IClienteGateway, ClienteGateway>();
            Services.AddScoped<IPedidoGateway, PedidoGateway>();
            Services.AddScoped<ICategoriaGateway, CategoriaGateway>();
            Services.AddScoped<IProdutoGateway, ProdutoGateway>();
            Services.AddScoped<IStatusGateway, StatusGateway>();
           */

            /* ***** serviços de negocio ***** */
            /*
            Services.AddScoped<IClienteUseCase, ClienteUseCase>();
            Services.AddScoped<ICategoriaUseCase, CategoriaUseCase>();
            Services.AddScoped<IProdutoUseCase, ProdutoUseCase>();
            Services.AddScoped<IMercadoPagoUseCase, MercadoPagoUseCase>();
            Services.AddScoped<IPagamentoUseCase, PagamentoUseCase>();
            Services.AddScoped<IPedidoUseCase, PedidoUseCase>();
            */
            //Services.AddScoped<IDataSource, DataSource.DataSource>();

           
        }

        public static IServiceCollection AddServiceMercadoPagoStructure(this IServiceCollection Services, IConfiguration configuration)
        {
            return Services;

            //Services.AddScoped<IMercadoPagoUseCase, MercadoPagoUseCase>();
            //return Services;
        }


    }
}

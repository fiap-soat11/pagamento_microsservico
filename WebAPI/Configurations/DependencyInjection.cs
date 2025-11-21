using Adapters.Controllers;
using Adapters.Controllers.Interfaces;
using Adapters.Gateways;
using Adapters.Gateways.Interfaces;
using Application.UseCases;
using DataSource.Context;
using DataSource.Repositories;
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
            Services.AddScoped<IQRCodeController, QRCodeController>();
            Services.AddScoped<IPagamentoController, PagamentoController>();

            /* ***** serviços de acesso a dados ***** */
            Services.AddScoped<IStatusGateway, StatusGateway>();


            /* ***** serviços de negocio ***** */
            Services.AddScoped<IMercadoPagoUseCase, MercadoPagoUseCase>();
            Services.AddScoped<IPagamentoUseCase, PagamentoUseCase>();
                    
            return Services;
        }

    }


}
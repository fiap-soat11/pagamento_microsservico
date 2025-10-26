using Adapters.Controllers.Interfaces;
using Application.Configurations;
using Application.UseCases;
using Microsoft.Extensions.Logging;

namespace Adapters.Controllers;

public class PagamentoController(ILogger<PagamentoController> logger, IMercadoPagoUseCase mercadoPagoUseCase) : IPagamentoController
{
    public async Task ConsultarPagamento(long pagamentoId)
    {
        if (pagamentoId <= 0)
            throw new BusinessException("Pagamento não informado");

        await mercadoPagoUseCase.ConsultarPagamento(pagamentoId);
    }
}
using Adapters.Controllers.Interfaces;
using Application.Configurations;
using Application.UseCases;
using Adapters.Gateways.Interfaces;
using Adapters.Presenters.Pagamento;
using Domain;
using Microsoft.Extensions.Logging;

namespace Adapters.Controllers;

public class PagamentoController(ILogger<PagamentoController> logger, IDataSource dataSource) : IPagamentoController
{
    private readonly ILogger<PagamentoController> _logger = logger;
    private readonly IDataSource _dataSource = dataSource;
    public async Task<PagamentoResponse> ConsultarPagamento(long pagamentoId)
    {
        _logger.LogInformation("Consultando pagamento {PagamentoId}", pagamentoId);
        if (pagamentoId <= 0)
            throw new BusinessException("Pagamento não informado");

        // Consultar pagamento via repositório (DataSource) ao invés do MercadoPagoUseCase
        var pagamento = _dataSource.BuscarPagamentoPorId((int)pagamentoId);
        if (pagamento == null)
            throw new BusinessException("Pagamento não encontrado");

        var response = new PagamentoResponse
        {
            IdPagamento = pagamento.IdPagamento,
            IdPedido = pagamento.IdPedido,
            ValorPago = pagamento.ValorPago,
            DataPagamento = pagamento.DataPagamento,
            IdFormaPagamento = pagamento.IdFormaPagamento,
            IdStatusPagamento = pagamento.IdStatusPagamento,
            Tentativa = pagamento.Tentativa
        };

        _logger.LogInformation("Pagamento {PagamentoId} encontrado", pagamentoId);
        return await Task.FromResult(response);
    }
}
using Adapters.Controllers.Interfaces;
using Adapters.Gateways.Interfaces;
using Adapters.Mappers;
using Adapters.Presenters.QRCode;
using Application.Configurations;
using Application.UseCases;
using MercadoPago.Client.Preference;
using Microsoft.Extensions.Logging;

namespace Adapters.Controllers
{
    public class QRCodeController(ILogger<QRCodeController> logger, 
        IMercadoPagoUseCase mercadoPagoUseCase,
        IDataSource dataSource) : IQRCodeController
    {
        private readonly ILogger<QRCodeController> _logger = logger;
        private readonly IDataSource _dataSource = dataSource;

        public async Task<QRCodeResponse> GerarQRCodePedido(int idPedido, decimal valorTotal, int quantidadeTotal)
        {
            if (idPedido <= 0)
                throw new BusinessException("Pedido informado");

            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = "Pedido de Teste - FIAP",
                        Quantity = quantidadeTotal,
                        UnitPrice =  valorTotal,
                    }
                },
                ExternalReference = idPedido.ToString()
            };

            _logger.LogInformation("Gerando QRCode para pedido {PedidoId}", idPedido);
            var result = await mercadoPagoUseCase.CriarQRCodeAsync(request);

            // Inserir registro de pagamento com defaults
            var pagamento = new Domain.Pagamento
            {
                IdPedido = idPedido,
                IdFormaPagamento = (int)Domain.Enums.FormaPagamentoTipo.Pix,
                IdStatusPagamento = (int)Domain.Enums.StatusPagamentoTipo.Pendente,
                ValorPago = valorTotal,
                DataPagamento = null,
                Tentativa = 0
            };

            _dataSource.InserirPagamento(pagamento);
            _logger.LogInformation("Pagamento inicial inserido para pedido {PedidoId}", idPedido);

            //pedido.QRCode = result.ImageBase64;

            //pedidoGateway.AtualizarPedido(pedido);

            return QRCodeMapper.QRCodeMapperDTO(result);
        }

        public async Task PagarQRCodePedido(int idPedido)
        {
            if (idPedido <= 0)
                throw new BusinessException("Pedido não informado.");

            try
            {
                await mercadoPagoUseCase.PagarQRCodeAsync(idPedido);
                var pagamento = _dataSource.BuscarPagamentoPorPedido(idPedido);
                if (pagamento != null)
                {
                    pagamento.DataPagamento = DateTime.UtcNow;
                    pagamento.IdStatusPagamento = (int)Domain.Enums.StatusPagamentoTipo.Aprovado;
                    _dataSource.AtualizarPagamento(pagamento);
                    _logger.LogInformation("Pagamento aprovado para pedido {PedidoId}", idPedido);
                }
                //   await pedidoGateway.AtualizarStatusPedido(3, idPedido);
            }
            catch (Exception)
            {
                var pagamento = _dataSource.BuscarPagamentoPorPedido(idPedido);
                if (pagamento != null)
                {
                    pagamento.DataPagamento = null;
                    pagamento.IdStatusPagamento = (int)Domain.Enums.StatusPagamentoTipo.Recusado;
                    pagamento.Tentativa = (pagamento.Tentativa ?? 0) + 1;
                    _dataSource.AtualizarPagamento(pagamento);
                    _logger.LogWarning("Pagamento recusado para pedido {PedidoId}", idPedido);
                }
                //if (pedido.Pagamentos.Any() && pedido.Pagamentos.FirstOrDefault().Tentativa.GetValueOrDefault() >= 5)
                //{
                //    await pedidoGateway.FinalizarPedido(idPedido);
                //}
                throw;
            }
        }
    }
}
using Adapters.Controllers.Interfaces;
using Adapters.Gateways.Interfaces;
using Adapters.Mappers;
using Adapters.Presenters.QRCode;
using Application.Configurations;
using Application.Interfaces;
using Application.UseCases;
using MercadoPago.Client.Preference;
using Microsoft.Extensions.Logging;

namespace Adapters.Controllers
{
    public class QRCodeController(ILogger<QRCodeController> logger, IPedidoGateway pedidoGateway, IMercadoPagoUseCase mercadoPagoUseCase) : IQRCodeController
    {

        public async Task<QRCodeResponse> GerarQRCodePedido(int idPedido)
        {
            if (idPedido <= 0)
                throw new BusinessException("Pedido informado");

            var pedido = await pedidoGateway.BuscarPedidoPorId(idPedido);
            if (pedido is null)
                throw new BusinessException("Pedido não existe.");

            var quantidadeTotal = pedidoGateway.CarregarTodosProdutosPedido(idPedido).Result.Sum(d => d.Quantidade.GetValueOrDefault());

            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = "Pedido de Teste - FIAP",
                        Quantity = quantidadeTotal,
                        UnitPrice =  pedido.ValorTotal.GetValueOrDefault(),
                    }
                },
                ExternalReference = idPedido.ToString()
            };

            var result = await mercadoPagoUseCase.CriarQRCodeAsync(request);

            pedido.QRCode = result.ImageBase64;

            pedidoGateway.AtualizarPedido(pedido);

            return QRCodeMapper.QRCodeMapperDTO(result);
        }

        public async Task PagarQRCodePedido(int idPedido)
        {
            if (idPedido <= 0)
                throw new BusinessException("Pedido não informado.");

            var pedido = await pedidoGateway.BuscarPedidoPorId(idPedido);

            if (pedido is null)
                throw new BusinessException("Pedido não existe.");

            if (string.IsNullOrEmpty(pedido.QRCode))
                throw new BusinessException("O Pedido não tem QRCode gerado.");

            try
            {
                await mercadoPagoUseCase.PagarQRCodeAsync(idPedido);
                await pedidoGateway.AtualizarStatusPedido(3, idPedido);
            }
            catch (Exception)
            {
                if (pedido.Pagamentos.Any() && pedido.Pagamentos.FirstOrDefault().Tentativa.GetValueOrDefault() >= 5)
                {
                    await pedidoGateway.FinalizarPedido(idPedido);
                }

                throw;
            }
        }
        public async Task NotificacaoPagamento()
        {

        }
    }
}
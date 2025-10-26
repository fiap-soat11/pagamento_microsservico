using Adapters.Controllers.Interfaces;
using Adapters.Presenters.Pedido;
using Adapters.Presenters.QRCode;
using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QRCodeControllerHandler : ControllerBase
    {
        private readonly ILogger<QRCodeControllerHandler> _logger;
        private readonly IMercadoPagoUseCase _mercadoPagoUseCase;
        public QRCodeControllerHandler(ILogger<QRCodeControllerHandler> logger, IMercadoPagoUseCase mercadoPagoUseCase)
        {
            _logger = logger;
            _mercadoPagoUseCase = mercadoPagoUseCase;          
        }

        [HttpPost("GerarQRCodePedido")]
        [ProducesResponseType(typeof(PedidoCozinhaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GerarQRCodePedido(int idPedido, [FromServices] IQRCodeController controller)
        {
            var result = await controller.GerarQRCodePedido(idPedido);

            return Ok(new
            {
                qr_code_base64 = result.ImageBase64,
                link_pagamento = result.Link
            });
        }

        [HttpPost("PagarQRCodePedido")]
        [ProducesResponseType(typeof(PedidoCozinhaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PagarQRCodePedido(int idPedido, [FromServices] IQRCodeController controller)
        {              
            try
            {
                await controller.PagarQRCodePedido(idPedido);

                return Ok($"O Pagamento do Pedido '{idPedido}' foi realizado com sucesso.");
            }
            catch (Exception)
            {
                /*
                if (pedido.Pagamentos.Any() && pedido.Pagamentos.FirstOrDefault().Tentativa.GetValueOrDefault() >= 5)
                {
                    await _pedidoUseCase.FinalizarPedido(pedido);

                    return Ok($"O Pedido '{idPedido}' foi cancelado, pois não conseguimos realizar o pagamento.");
                }               
                */
                throw;
            }
        }

        

        #region Method's


        #endregion
    }
}

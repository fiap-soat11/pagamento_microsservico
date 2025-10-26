using Adapters.Presenters.Pedido;
using Microsoft.AspNetCore.Mvc;
using Adapters.Mappers;
using Application.Configurations;
using Adapters.Controllers.Interfaces;
using WebAPI.Mappers;
using Adapters.Presenters.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PedidoControllerHandler : ControllerBase
    {
        private readonly ILogger<PedidoControllerHandler> _logger;
        private readonly IPedidoController _pedidoController;

        public PedidoControllerHandler(ILogger<PedidoControllerHandler> logger, IPedidoController pedidoController)
        {
            _logger = logger;
            _pedidoController = pedidoController;
        }

        [HttpGet("ListarPedidos")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> ListarPedidos()
        {
            var pedidos = await _pedidoController.ListarPedidos();
            if (pedidos == null || !pedidos.Any())
            {
                return NotFound();
            }
            var pedidosResponse = pedidos.Select(pedido => PedidoMapper.ToResponse(pedido));
            return Ok(pedidosResponse);
        }

        [HttpGet("ListarPedidosCliente")]
        [ProducesResponseType(typeof(PedidoClienteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> ListarPedidosClientes()
        {
            var pedidos = await _pedidoController.ListarPedidoClienteStatus();
            if (pedidos == null || !pedidos.Any())
            {
                return NotFound();
            }
            var pedidosResponse = pedidos.Select(pedido => PedidoMapper.PedidoClienteToDTO(pedido));
            return Ok(pedidosResponse);
        }


        [HttpGet("ListarPedidosCozinha")]
        [ProducesResponseType(typeof(PedidoCozinhaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> ListarPedidosCozinha()
        {
            var pedidos = await _pedidoController.ListarPedidoCozinha();
            if (pedidos == null || !pedidos.Any())
            {
                return NotFound();
            }
            var pedidosResponse = pedidos.Select(pedido => PedidoMapper.PedidoCozinhaToDTO(pedido));
            return Ok(pedidosResponse);
        }

        [HttpPost("IniciarPedido")]
        [ProducesResponseType(typeof(PedidoClienteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> IniciarPedido(string? cpf)
        {
            try
            {
                var pedido = await _pedidoController.IniciarPedido(cpf);

                var pedidoResponse = PedidoMapper.PedidoClienteToDTO(pedido);

                return Ok(pedidoResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pedido");
                return BadRequest("Erro ao atualizar pedido");
            }

        }


        [HttpPost("AtualizarPedido")]
        [ProducesResponseType(typeof(PedidoCozinhaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> AtualizarStatusPedido(int idPedido, PedidoRequest request)
        {
            try
            {
                var pedido = await _pedidoController
                .AtualizarStatusPedido(request.StatusId, idPedido);
                if (pedido == false)
                    return NotFound();
               
                return Ok("Status do pedido atualizado com sucesso"); 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pedido");
                return BadRequest("Erro ao atualizar pedido");
            }
           
        }

        [HttpPost("FinalizarPedido")]
        [ProducesResponseType(typeof(PedidoCozinhaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> FinalizarPedido(int idPedido)
        {
            try
            {
                var pedido = await _pedidoController.FinalizarPedido(idPedido);
                if (pedido == false)
                    return NotFound();

                return Ok("Pedido finalizado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao finalizar pedido");
                return BadRequest("Erro ao finalizar pedido");
            }

        }


        [HttpPost("CancelarPedido")]
        [ProducesResponseType(typeof(PedidoCozinhaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CancelarPedido(int idPedido)
        {
            try
            {

                var pedido = await _pedidoController.CancelarPedido(idPedido);
                if (pedido == false)
                    return NotFound();

                return Ok("Pedido cancelado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cancelado pedido");
                return BadRequest("Erro ao cancelado pedido");
            }

        }

        [HttpPost("{idPedido}/produtos")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AdicionarProduto(int idPedido, [FromBody] AdicionarProdutoPedidoRequest request)
        {
            try
            {
                var pedidoAtualizado = await _pedidoController.AdicionarProduto(idPedido, request.IdProduto, request.Quantidade, request.Observacao);
                return Ok(PedidoMapper.ToResponse(pedidoAtualizado));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{idPedido}/produtos/{idPedidoProduto}")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarProduto(int idPedido, int idPedidoProduto, [FromBody] AtualizarProdutoPedidoRequest request)
        {
            try
            {
                var pedidoAtualizado = await _pedidoController.AtualizarProduto(idPedido, idPedidoProduto, request.NovaQuantidade, request.Observacao);
                return Ok(PedidoMapper.ToResponse(pedidoAtualizado));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idPedido}/produtos/{idPedidoProduto}")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoverProduto(int idPedido, int idPedidoProduto)
        {
            try
            {
                var pedidoAtualizado = await _pedidoController.RemoverProduto(idPedido, idPedidoProduto);
                return Ok(PedidoMapper.ToResponse(pedidoAtualizado));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

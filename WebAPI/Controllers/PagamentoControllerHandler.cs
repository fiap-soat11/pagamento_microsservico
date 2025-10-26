using Adapters.Controllers.Interfaces;
using Adapters.Presenters.QRCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PagamentoControllerHandler : ControllerBase
    {
        private readonly ILogger<PagamentoControllerHandler> _logger;

        public PagamentoControllerHandler(ILogger<PagamentoControllerHandler> logger)
        {
            _logger = logger;            
        }

        [HttpPost("WehookNotificacaoPagamento")]
        [ProducesResponseType(typeof(NotificacaoPagamentoRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> NotificacaoPagamentoAsync([FromBody] NotificacaoPagamentoRequest body, [FromServices] IPagamentoController controller)
        {
            Console.WriteLine("Webhook recebido: " + body.ToString());

            if (body is not null)            
               await controller.ConsultarPagamento(body.Id);            

            return Ok();
        }
    }
}

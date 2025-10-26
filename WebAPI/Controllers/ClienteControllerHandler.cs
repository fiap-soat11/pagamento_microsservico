using Application.Configurations;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Adapters.Presenters.Cliente;
using WebAPI.Mappers;
using Adapters.Controllers.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/Cliente")]
public class ClienteControllerHandler : ControllerBase
{
    private readonly ILogger<ClienteControllerHandler> _logger;
    private readonly IClienteController _clienteController;

    public ClienteControllerHandler(ILogger<ClienteControllerHandler> logger,
                                    IClienteController clienteController)
    {
        _logger = logger;
        _clienteController = clienteController;

    }

    [HttpGet(Name = "ListarClientes")]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> ListarClientes()
    {
        var clientes = await _clienteController.ListarClientes();
        if (clientes == null || !clientes.Any())
            return NotFound();
        return Ok(clientes);
    }

    [HttpGet("{cpf}", Name = "BuscarCliente")]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> BuscarCliente(string cpf)
    {
        try
        {
            var cliente = await _clienteController.BuscarCliente(cpf);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar cliente");
            return BadRequest("Erro ao buscar cliente");
        }
        
    }

    [HttpPost(Name = "IncluirCliente")]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Post([FromBody] ClienteRequest cliente)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            await _clienteController.IncluirCliente(cliente);
            return Ok("Cliente inclu�do com sucesso");
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, "Erro ao incluir cliente");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao incluir cliente");
            return BadRequest("Erro ao incluir cliente");
        }
    }

    [HttpPut("{cpf}", Name = "AtualizarCliente")]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Consumes("application/json")]    
    public async Task<IActionResult> Put(string cpf, [FromBody] ClienteRequest cliente)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _clienteController.AtualizarCliente(cliente);
            return Ok("Cliente atualizado com sucesso");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar cliente");
            return BadRequest("Erro ao atualizar cliente");
        }
        
    }

    [HttpDelete("{cpf}", Name = "ExcluirCliente")]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(string cpf)
    {
        try
        {
            await _clienteController.ExcluirCliente(cpf);
            return Ok("Cliente exclu�do com sucesso");
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, "Erro ao excluir cliente");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir cliente");
            return BadRequest("Erro ao excluir cliente");
        }
        
    }



}

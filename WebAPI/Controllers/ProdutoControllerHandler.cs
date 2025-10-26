using Application.Configurations;
using Microsoft.AspNetCore.Mvc;
using Adapters.Presenters.Cliente;
using Adapters.Controllers.Interfaces;
using Adapters.Presenters.Categoria;
using Adapters.Presenters.Produto;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/Produto")]
public class ProdutoControllerHandler : ControllerBase
{
    private readonly ILogger<ProdutoControllerHandler> _logger;
    private readonly IProdutoController _produtoController;

    public ProdutoControllerHandler(ILogger<ProdutoControllerHandler> logger, IProdutoController produtoController)
    {
        _logger = logger;
        _produtoController = produtoController;

    }
   
    [HttpGet(Name = "ListarTodosProdutos")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> ListarProdutos()
    {
        var produtos = await _produtoController.ListarTodos();      

        return Ok(produtos);
    }

    [HttpGet("{idProduto}", Name = "BuscarProdutoPorId")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> BuscarProduto(int idProduto)
    {
        try
        {
            var produto = await _produtoController.BuscarProdutoPorId(idProduto);
         
            return Ok(produto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar Produto");
            return BadRequest("Erro ao buscar Produto");
        }
        
    }
    
    [HttpGet("idCategoria/{idCategoria}", Name = "ListarProdutosPorCategoria")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> BuscarProdutosPorCategoria(int idCategoria)
    {
        try
        {
            var produtos = await _produtoController.BuscarProdutosPorCategoria(idCategoria);

            return Ok(produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar Produto");
            return BadRequest("Erro ao buscar Produto");
        }

    }

    [HttpPost(Name = "IncluirProduto")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Post([FromBody] ProdutoRequest produto)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            await _produtoController.IncluirProduto(produto);
            return Ok("Produto incluido com sucesso");
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, "Erro ao incluir Produto");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao incluir Produto");
            return BadRequest("Erro ao incluir Produto");
        }
    }

    [HttpPut("AtualizarProduto", Name = "AtualizarProduto")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Consumes("application/json")]    
    public async Task<IActionResult> Put([FromBody] ProdutoRequest produto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _produtoController.AtualizarProduto(produto);
            return Ok("Produto atualizado com sucesso");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar Produto");
            return BadRequest("Erro ao atualizar Produto");
        }
        
    }

    [HttpDelete("ExcluirProduto/{idProduto}", Name = "ExcluirProduto")]
    [ProducesResponseType(typeof(ClienteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(int idProduto)
    {
        try
        {
            await _produtoController.ExcluirProduto(idProduto);
            return Ok("Produto excluido com sucesso");
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, "Erro ao excluir Produto");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir Produto");
            return BadRequest("Erro ao excluir Produto");
        }        
    }
}
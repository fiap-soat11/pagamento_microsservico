using Adapters.Controllers.Interfaces;
using Adapters.Presenters.Categoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Mappers;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CategoriaControllerHandler : ControllerBase
    {
        private readonly ILogger<CategoriaControllerHandler> _logger;
        private readonly ICategoriaController _categoriaController;      

        public CategoriaControllerHandler(ILogger<CategoriaControllerHandler> logger, ICategoriaController categoriaController)
        {
            _logger = logger;
            _categoriaController = categoriaController;              
        }

        [HttpGet("ListarTodosCategoria")]
        [ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> ListarCategoria()
        {
            var categorias = await _categoriaController.ListarTodos();
            if (categorias == null || !categorias.Any())
            {
                return NotFound();
            }
            var categoriaResponse = categorias.Select(categoria => CategoriaMapper.ToDTO(categoria));
            return Ok(categoriaResponse);
        }

        /*[HttpGet("ListarProdutos")]
        [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<IActionResult> ListarProdutos(int idCategoria)
        {
            var produtos = await _produtoUseCase.BuscarProdutoPorCategoria(idCategoria);
            if (produtos == null || !produtos.Any())
            {
                return NotFound();
            }
            var produtoResponse = produtos.Select(produto => ProdutoMapper.ToDTO(produto));
            return Ok(produtoResponse);
        }*/
    
        //[HttpPost("FinalizarCategoria")]
        //[ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        //public async Task<IActionResult> FinalizarCategoria(int IdCategoria)
        //{
        //    try
        //    {
        //        var categoria = await _categoriaUseCase.BuscarCategoriaPorId(IdCategoria);
        //        if (categoria == null)
        //            return NotFound();         

        //        await _categoriaUseCase.FinalizarCategoria(categoria);
        //        return Ok("Categoria finalizado com sucesso");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao finalizar categoria");
        //        return BadRequest("Erro ao finalizar categoria");
        //    }

        //}


        //[HttpPost("CancelarCategoria")]
        //[ProducesResponseType(typeof(CategoriaResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Produces("application/json")]
        //[Consumes("application/json")]
        //public async Task<IActionResult> CancelarCategoria(int IdCategoria)
        //{
        //    try
        //    {
        //        var categoria = await _categoriaUseCase.BuscarCategoriaPorId(IdCategoria);
        //        if (categoria == null)
        //            return NotFound();

        //        await _categoriaUseCase.FinalizarCategoria(categoria);
        //        return Ok("Categoria cancelado com sucesso");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Erro ao cancelar categoria");
        //        return BadRequest("Erro ao cancelado categoria");
        //    }

        //}
    }
}

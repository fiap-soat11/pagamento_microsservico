namespace Adapters.Controllers.Interfaces;

public interface IPagamentoController
{
    Task ConsultarPagamento(long pagamentoId);
}
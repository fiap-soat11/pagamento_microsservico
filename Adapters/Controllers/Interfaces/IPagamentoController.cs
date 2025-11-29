using Adapters.Presenters.Pagamento;

namespace Adapters.Controllers.Interfaces;

public interface IPagamentoController
{
    Task<PagamentoResponse> ConsultarPagamento(long pagamentoId);
}
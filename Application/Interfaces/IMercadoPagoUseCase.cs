using Domain.Entities;
using MercadoPago.Client.Preference;

namespace Application.UseCases
{
    public interface IMercadoPagoUseCase
    {
        Task<QrCode> CriarQRCodeAsync(PreferenceRequest request);
        Task PagarQRCodeAsync(int idPedido);
        Task ConsultarPagamento(long pagamentoId);
    }
}
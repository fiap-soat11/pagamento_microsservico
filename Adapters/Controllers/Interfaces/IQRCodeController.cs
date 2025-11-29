using Adapters.Presenters.QRCode;
using Domain;

namespace Adapters.Controllers.Interfaces
{
    public interface IQRCodeController
    {
        Task<QRCodeResponse> GerarQRCodePedido(int idPedido, decimal valorTotal, int quantidadeTotal);
        Task PagarQRCodePedido(int idPedido);
    }
}

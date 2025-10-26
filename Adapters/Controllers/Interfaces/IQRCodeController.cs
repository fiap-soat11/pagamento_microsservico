using Adapters.Presenters.QRCode;
using Domain;

namespace Adapters.Controllers.Interfaces
{
    public interface IQRCodeController
    {
        Task<QRCodeResponse> GerarQRCodePedido(int idPedido);
        Task PagarQRCodePedido(int idPedido);
    }
}

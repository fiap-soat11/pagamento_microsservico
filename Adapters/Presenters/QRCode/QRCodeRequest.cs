namespace Adapters.Presenters.QRCode
{
    public class QRCodeRequest
    {
        public int Pedido { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ExternalReference { get; set; } = string.Empty;
    }
}

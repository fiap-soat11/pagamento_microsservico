using Adapters.Controllers;
using Adapters.Controllers.Interfaces;
using Adapters.Gateways.Interfaces;
using Adapters.Presenters.Pagamento;
using Application.UseCases;
using Domain;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests
{
    [TestClass]
    public class ControllersTests
    {
        private Mock<ILogger<QRCodeController>> _qrLogger = new Mock<ILogger<QRCodeController>>();
        private Mock<ILogger<PagamentoController>> _pgLogger = new Mock<ILogger<PagamentoController>>();
        private Mock<IMercadoPagoUseCase> _mp = new Mock<IMercadoPagoUseCase>();
        private Mock<IDataSource> _ds = new Mock<IDataSource>();

        [TestMethod]
        public async Task QRCodeController_GerarQRCodePedido_DeveInserirPagamentoPendente()
        {
            _mp.Setup(m => m.CriarQRCodeAsync(It.IsAny<MercadoPago.Client.Preference.PreferenceRequest>()))
               .ReturnsAsync(new Domain.Entities.QrCode("base64", "https://link"));

            var ctrl = new QRCodeController(_qrLogger.Object, _mp.Object, _ds.Object);
            var resp = await ctrl.GerarQRCodePedido(10, 100m, 2);

            _ds.Verify(d => d.InserirPagamento(It.Is<Pagamento>(p => p.IdPedido == 10 && p.IdFormaPagamento == (int)FormaPagamentoTipo.Pix && p.IdStatusPagamento == (int)StatusPagamentoTipo.Pendente && p.DataPagamento == null)), Times.Once);
            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public async Task PagamentoController_ConsultarPagamento_DeveRetornarDTO()
        {
            _ds.Setup(d => d.BuscarPagamentoPorId(1)).Returns(new Pagamento
            {
                IdPagamento = 1,
                IdPedido = 10,
                ValorPago = 100m,
                IdFormaPagamento = (int)FormaPagamentoTipo.Pix,
                IdStatusPagamento = (int)StatusPagamentoTipo.Pendente
            });

            var ctrl = new PagamentoController(_pgLogger.Object, _ds.Object);
            PagamentoResponse resp = await ctrl.ConsultarPagamento(1);

            Assert.AreEqual(1, resp.IdPagamento);
            Assert.AreEqual(10, resp.IdPedido);
            Assert.AreEqual(100m, resp.ValorPago);
        }

        [TestMethod]
        public async Task QRCodeController_PagarQRCodePedido_Sucesso_DeveAprovar()
        {
            _mp.Setup(m => m.PagarQRCodeAsync(It.IsAny<int>())).Returns(Task.CompletedTask);
            var ctrl = new QRCodeController(_qrLogger.Object, _mp.Object, _ds.Object);

            var pagamentoExistente = new Pagamento { IdPedido = 20, IdStatusPagamento = (int)StatusPagamentoTipo.Pendente, Tentativa = 0 };
            _ds.Setup(d => d.BuscarPagamentoPorPedido(20)).Returns(pagamentoExistente);

            await ctrl.PagarQRCodePedido(20);

            _ds.Verify(d => d.AtualizarPagamento(It.Is<Pagamento>(p => p.IdPedido == 20 && p.IdStatusPagamento == (int)StatusPagamentoTipo.Aprovado && p.DataPagamento != null)), Times.Once);
        }

        [TestMethod]
        public async Task QRCodeController_PagarQRCodePedido_Falha_DeveRecusarEIncrementarTentativa()
        {
            _mp.Setup(m => m.PagarQRCodeAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Erro ao fazer o pagamento."));
            var ctrl = new QRCodeController(_qrLogger.Object, _mp.Object, _ds.Object);

            var pagamentoExistente = new Pagamento { IdPedido = 30, IdStatusPagamento = (int)StatusPagamentoTipo.Pendente, Tentativa = 1 };
            _ds.Setup(d => d.BuscarPagamentoPorPedido(30)).Returns(pagamentoExistente);

            try
            {
                await ctrl.PagarQRCodePedido(30);
            }
            catch
            {
                // esperado
            }

            _ds.Verify(d => d.AtualizarPagamento(It.Is<Pagamento>(p => p.IdPedido == 30 && p.IdStatusPagamento == (int)StatusPagamentoTipo.Recusado && p.DataPagamento == null && p.Tentativa == 2)), Times.Once);
        }
    }
}

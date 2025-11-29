using Adapters.Gateways.Interfaces;
using DS = DataSource;
using DataSource.Repositories.Interfaces;
using Domain;
using Moq;

namespace Tests
{
    [TestClass]
    public class DataSourceTests
    {
        private Mock<IFormaPagamentoRepository> _formaRepo = new Mock<IFormaPagamentoRepository>();
        private Mock<IPagamentoRepository> _pagRepo = new Mock<IPagamentoRepository>();
        private Mock<IStatusPagamentoRepository> _statusRepo = new Mock<IStatusPagamentoRepository>();

        [TestMethod]
        public void Delegacao_FormaPagamento_DeveChamarRepositorio()
        {
            var ds = new DS.DataSource(_formaRepo.Object, _pagRepo.Object, _statusRepo.Object);
            var forma = new FormaPagamento { IdFormaPagamento = 1, Nome = "Pix" };

            ds.InserirFormaPagamento(forma);
            _formaRepo.Verify(r => r.Inserir(forma), Times.Once);

            ds.BuscarFormaPagamentoPorId(1);
            _formaRepo.Verify(r => r.BuscarPorId(1), Times.Once);
        }

        [TestMethod]
        public void Delegacao_StatusPagamento_DeveChamarRepositorio()
        {
            var ds = new DS.DataSource(_formaRepo.Object, _pagRepo.Object, _statusRepo.Object);
            var status = new StatusPagamento { IdStatusPagamento = 1, Nome = "Pendente" };

            ds.InserirStatusPagamento(status);
            _statusRepo.Verify(r => r.Inserir(status), Times.Once);

            ds.ExcluirStatusPagamento(1);
            _statusRepo.Verify(r => r.Excluir(1), Times.Once);
        }

        [TestMethod]
        public void Delegacao_Pagamento_DeveChamarRepositorio()
        {
            var ds = new DS.DataSource(_formaRepo.Object, _pagRepo.Object, _statusRepo.Object);
            var pagamento = new Pagamento { IdPedido = 10 };

            ds.InserirPagamento(pagamento);
            _pagRepo.Verify(r => r.Inserir(pagamento), Times.Once);

            ds.BuscarPagamentoPorId(5);
            _pagRepo.Verify(r => r.BuscarPorId(5), Times.Once);
        }
    }
}

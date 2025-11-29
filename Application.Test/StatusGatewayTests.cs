using Adapters.Gateways;
using Adapters.Gateways.Interfaces;
using Domain;
using Moq;

namespace Tests
{
    [TestClass]
    public class StatusGatewayTests
    {
        [TestMethod]
        public void ListarTodos_DeveDelegarParaDataSource()
        {
            var ds = new Mock<IDataSource>();
            ds.Setup(d => d.ListarStatusPagamento()).Returns(new List<StatusPagamento> { new StatusPagamento { IdStatusPagamento = 1, Nome = "Pendente" } });

            var gateway = new StatusGateway(ds.Object);
            var lista = gateway.ListarTodos();

            Assert.AreEqual(1, lista.Count());
            ds.Verify(d => d.ListarStatusPagamento(), Times.Once);
        }

        [TestMethod]
        public void Inserir_Atualizar_Excluir_DeveDelegar()
        {
            var ds = new Mock<IDataSource>();
            var gateway = new StatusGateway(ds.Object);
            var status = new StatusPagamento { IdStatusPagamento = 2, Nome = "Aprovado" };

            gateway.Inserir(status);
            ds.Verify(d => d.InserirStatusPagamento(status), Times.Once);

            gateway.Atualizar(status);
            ds.Verify(d => d.AtualizarStatusPagamento(status), Times.Once);

            gateway.Excluir(2);
            ds.Verify(d => d.ExcluirStatusPagamento(2), Times.Once);
        }
    }
}

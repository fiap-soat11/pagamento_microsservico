using DataSource.Context;
using DataSource.Repositories;
using DataSource.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    [TestClass]
    public class PagamentoRepositoryTests
    {
        private ApplicationDbContext _db;
        private IPagamentoRepository _repo;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _db = new ApplicationDbContext(options);
            _repo = new PagamentoRepository(_db);

            // Seed basic related entities
            _db.Add(new FormaPagamento { IdFormaPagamento = 1, Nome = "Pix", Ativo = true });
            _db.Add(new StatusPagamento { IdStatusPagamento = 1, Nome = "Pendente" });
            _db.SaveChanges();
        }

        [TestMethod]
        public void InserirEBuscarPagamento_DeveFuncionar()
        {
            var pagamento = new Pagamento
            {
                IdPedido = 100,
                IdFormaPagamento = 1,
                IdStatusPagamento = 1,
                ValorPago = 50m,
                Tentativa = 0
            };

            _repo.Inserir(pagamento);
            var encontrado = _repo.Buscar(p => p.IdPedido == 100).FirstOrDefault();
            Assert.IsNotNull(encontrado);
            Assert.AreEqual(50m, encontrado!.ValorPago);
        }

        [TestMethod]
        public void ListarComRelacionamentos_DeveTrazerNavegacoes()
        {
            var pagamento = new Pagamento
            {
                IdPedido = 200,
                IdFormaPagamento = 1,
                IdStatusPagamento = 1,
                ValorPago = 10m
            };
            _repo.Inserir(pagamento);

            var pr = (PagamentoRepository)_repo;
            var list = pr.ListarComRelacionamentos(p => p.IdPedido == 200).ToList();
            Assert.AreEqual(1, list.Count);
            Assert.IsNotNull(list[0].IdFormaPagamentoNavigation);
            Assert.IsNotNull(list[0].IdStatusPagamentoNavigation);
        }
    }
}

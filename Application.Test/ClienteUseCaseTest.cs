using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.UseCases;
using Domain;
using System;
using System.Threading.Tasks;

namespace Application.Tests.UseCases
{
    [TestClass]
    public class ClienteUseCaseTests
    {
        private ClienteUseCase _clienteUseCase;

        [TestInitialize]
        public void Setup()
        {
            _clienteUseCase = new ClienteUseCase();
        }

        [TestMethod]
        public async Task IncluirCliente_ValidData_ReturnsCliente()
        {
            // Arrange
            var cliente = new Cliente
            {
                Cpf = "12345678900",
                Nome = "João",
                Email = "joao@email.com",
                Ativo = true
            };

            // Act
            var result = await _clienteUseCase.IncluirCliente(cliente);

            // Assert
            Assert.AreEqual(cliente, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task IncluirCliente_NullCliente_ThrowsException()
        {
            await _clienteUseCase.IncluirCliente(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task IncluirCliente_EmptyCpf_ThrowsException()
        {
            var cliente = new Cliente { Cpf = "", Nome = "Teste", Email = "teste@email.com" };
            await _clienteUseCase.IncluirCliente(cliente);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task IncluirCliente_EmptyNome_ThrowsException()
        {
            var cliente = new Cliente { Cpf = "123", Nome = "", Email = "teste@email.com" };
            await _clienteUseCase.IncluirCliente(cliente);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task IncluirCliente_EmptyEmail_ThrowsException()
        {
            var cliente = new Cliente { Cpf = "123", Nome = "Teste", Email = "" };
            await _clienteUseCase.IncluirCliente(cliente);
        }

        [TestMethod]
        public async Task AtualizarCliente_ValidData_ReturnsUpdatedCliente()
        {
            var clienteOriginal = new Cliente
            {
                Cpf = "123",
                Nome = "João",
                Email = "joao@email.com",
                Ativo = true
            };

            var clienteAtualizado = new Cliente
            {
                Cpf = "123",
                Nome = "João da Silva",
                Email = "joao.silva@email.com",
                Ativo = false
            };

            var result = await _clienteUseCase.AtualizarCliente(clienteOriginal, clienteAtualizado);

            Assert.AreEqual(clienteAtualizado.Nome, result.Nome);
            Assert.AreEqual(clienteAtualizado.Email, result.Email);
            Assert.AreEqual(clienteAtualizado.Ativo, result.Ativo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task AtualizarCliente_NullCliente_ThrowsException()
        {
            var novoCliente = new Cliente { Cpf = "123", Nome = "Novo", Email = "novo@email.com" };
            await _clienteUseCase.AtualizarCliente(null, novoCliente);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task AtualizarCliente_CpfDifferent_ThrowsException()
        {
            var clienteOriginal = new Cliente { Cpf = "123" };
            var clienteNovo = new Cliente { Cpf = "456" };

            await _clienteUseCase.AtualizarCliente(clienteOriginal, clienteNovo);
        }

        [TestMethod]
        public async Task ExcluirCliente_ValidCliente_ReturnsClienteWithAtivoFalse()
        {
            var cliente = new Cliente { Cpf = "123", Nome = "Cliente", Ativo = true };

            var result = await _clienteUseCase.ExcluirCliente(cliente);

            Assert.IsFalse(result.Ativo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ExcluirCliente_NullCliente_ThrowsException()
        {
            await _clienteUseCase.ExcluirCliente(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ExcluirCliente_EmptyCpf_ThrowsException()
        {
            var cliente = new Cliente { Cpf = "", Nome = "Cliente", Ativo = true };

            await _clienteUseCase.ExcluirCliente(cliente);
        }
    }
}

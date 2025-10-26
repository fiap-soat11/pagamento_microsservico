using Application.UseCases;
using MercadoPago.Client.Preference;
using Microsoft.Extensions.Configuration;

namespace Tests
{
    [TestClass]
    public class MercadoPagoUseCaseTests
    {
        private MercadoPagoUseCase _useCase;
        

        [TestInitialize]
        public void Setup()
        {
            var configData = new Dictionary<string, string>
            {
                { "MercadoPago:AccessToken", "TEST-8420853824501796-051613-5f6f019edb58c54234dd89846311f809-151969256" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();

            //_useCase = new MercadoPagoUseCase(configuration);
        }

        [TestMethod]
        public async Task CriarQRCodeAsync_DeveRetornarBase64ComLink()
        {
            var preferenceRequest = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = "Produto Teste",
                        Quantity = 1,
                        CurrencyId = "BRL",
                        UnitPrice = 10
                    }
                }
            };

            var qrCode = await _useCase.CriarQRCodeAsync(preferenceRequest);

            Assert.IsNotNull(qrCode);
            Assert.IsTrue(qrCode.ImageBase64.StartsWith("data:image/png;base64,"));
            Assert.IsTrue(qrCode.Link.StartsWith("https://"));
        }

        [TestMethod]
        public async Task PagarQRCodeAsync_DeveLancarOkComCartaoTeste()
        {
            try
            {
                await _useCase.PagarQRCodeAsync(123);
                Assert.IsTrue(true);
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("Erro ao fazer o pagamento.", ex.Message);
            }
        }
    }
}

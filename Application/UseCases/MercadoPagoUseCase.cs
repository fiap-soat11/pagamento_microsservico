using Domain.Entities;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Microsoft.Extensions.Configuration;
using QRCoder;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Application.UseCases
{
    public class MercadoPagoUseCase : IMercadoPagoUseCase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public MercadoPagoUseCase(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            MercadoPagoConfig.AccessToken = configuration["MercadoPago:AccessToken"];
            _httpClientFactory = httpClientFactory;
        }

        public async Task<QrCode> CriarQRCodeAsync(PreferenceRequest request)
        {           

            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);

            string linkPagamento = preference.InitPoint;
            
            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(linkPagamento, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);
            string base64 = Convert.ToBase64String(qrCodeBytes);

            var qrCodeResult = new QrCode($"data:image/png;base64,{base64}", linkPagamento);

            return qrCodeResult;
        }

        public async Task PagarQRCodeAsync(int idPedido)
        {
           await PagamentoPorCartao(idPedido);
        }

        public async Task ConsultarPagamento(long pagamentoId)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MercadoPagoConfig.AccessToken);

            var response = await client.GetAsync($"https://api.mercadopago.com/v1/payments/{pagamentoId}");
            string result = await response.Content.ReadAsStringAsync();            
        }


        #region Private Method's

        private async Task PagamentoPorCartao(int idPedido)
        {            
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());
            var url = $"https://api.mercadopago.com/v1/payments?access_token={MercadoPagoConfig.AccessToken}";
            var cardToken = await GerarCardToken();

            var body = new
            {
                transaction_amount = 100,
                token = cardToken,
                description = "Pagamento de teste com cartão",
                installments = 1,
                payment_method_id = "visa",
                payer = new
                {
                    email = "ronaldo.oliveira92@hotmail.com"
                },
                external_reference = idPedido.ToString()
            };

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)               
                  throw new Exception(responseString);
            }
            catch (Exception)
            {
                throw new Exception("Erro ao fazer o pagamento.");
            }
        }
        private async Task<string> GerarCardToken()
        {
            using var client = new HttpClient();
            var url = $"https://api.mercadopago.com/v1/card_tokens?access_token={MercadoPagoConfig.AccessToken}";

            var body = new
            {
                card_number = "4235647728025682",
                expiration_year = 2030,
                expiration_month = 11,
                security_code = "123",
                cardholder = new
                {
                    name = "APRO"
                }
            };            

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return null;           

            using var doc = JsonDocument.Parse(responseString);
            string token = doc.RootElement.GetProperty("id").GetString();

            return token;
        }

        #endregion
    }
}
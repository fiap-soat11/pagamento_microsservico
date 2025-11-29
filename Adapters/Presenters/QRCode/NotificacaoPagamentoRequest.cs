using System.Text.Json.Serialization;

namespace Adapters.Presenters.QRCode
{
    public class NotificacaoPagamentoRequest
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("live_mode")]
        public bool LiveMode { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("api_version")]
        public required string ApiVersion { get; set; }

        [JsonPropertyName("action")]
        public required string Action { get; set; }

        [JsonPropertyName("data")]
        public required DadosNotificacaoRequest Data { get; set; }
    }

    public class  DadosNotificacaoRequest
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }
    }
}

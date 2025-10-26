using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NotificacaoPagamento
    {
        public int Id { get; set; }
        public bool LiveMode { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.MinValue;
        public int UserId { get; set; }
        public string ApiVersion { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public DadosNotificacao Data { get; set; } = new DadosNotificacao();

    }

    public class DadosNotificacao
    {
        public string Id { get; set; } = string.Empty;

    }
}

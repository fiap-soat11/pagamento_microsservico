namespace Domain.Enums
{
    // Representa os estados canônicos do pagamento
    // Deve manter correspondência com registros de "Status_Pagamento" na base.
    public enum StatusPagamentoTipo
    {
        Pendente = 1,
        Aprovado = 2,
        Recusado = 3,
        Cancelado = 4,
        Estornado = 5
    }
}

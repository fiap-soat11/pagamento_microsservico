namespace Domain.Enums
{
    // Representa tipos canônicos de forma de pagamento no domínio.
    // Deve manter correspondência com registros de "Forma_pagamento" na base.
    public enum FormaPagamentoTipo
    {
        Pix = 1,
        CartaoCredito = 2,
        CartaoDebito = 3,
        Dinheiro = 4,
        ValeRefeicao = 5
    }
}

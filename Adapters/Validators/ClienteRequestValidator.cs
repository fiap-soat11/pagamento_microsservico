using FluentValidation;
using System.Net;
using Adapters.Presenters.Cliente;

namespace Adapters.Validators
{
    public class ClienteRequestValidator : AbstractValidator<ClienteRequest>
    {

        public ClienteRequestValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .WithMessage("O CPF é obrigatório.")
                .Length(11)
                .WithMessage("O CPF deve ter 11 dígitos.")
                .Must(IsValidCpf).WithMessage("CPF inválido.");
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O Nome é obrigatório.");
            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("O e-mail deve ser válido.");
        }

        private bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2) resto = 0;
            else resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

    }
}

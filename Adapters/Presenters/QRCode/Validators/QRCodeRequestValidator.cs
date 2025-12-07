using Adapters.Presenters.QRCode;
using FluentValidation;

namespace Adapters.Presenters.QRCode.Validators
{
    public class QRCodeRequestValidator : AbstractValidator<QRCodeRequest>
    {
        public QRCodeRequestValidator()
        {
            RuleFor(x => x.Pedido).GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.UnitPrice).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.ExternalReference).NotEmpty().MaximumLength(100);
        }
    }
}

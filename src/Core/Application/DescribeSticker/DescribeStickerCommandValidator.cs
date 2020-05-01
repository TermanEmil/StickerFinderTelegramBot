using FluentValidation;

namespace Application.DescribeSticker
{
    public class DescribeStickerCommandValidator : AbstractValidator<DescribeStickerCommand>
    {
        public DescribeStickerCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
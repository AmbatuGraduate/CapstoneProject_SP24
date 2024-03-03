using FluentValidation;

namespace Application.Street.Commands.Add
{
    public class AddStreetValidator : AbstractValidator<AddStreetCommand>
    {
        /// Set rule for request addStreet
        public AddStreetValidator()
        {
            RuleFor(x => x.StreetName).NotEmpty();
            RuleFor(x => x.StreetLength).NotEmpty();
            RuleFor(x => x.NumberOfHouses).NotEmpty();
            RuleFor(x => x.StreetTypeId).NotEmpty();
            RuleFor(x => x.WardId).NotEmpty();
            RuleFor(x => x.CreateBy).NotEmpty();
            RuleFor(x => x.UpdateBy).NotEmpty();
        }
    }
}


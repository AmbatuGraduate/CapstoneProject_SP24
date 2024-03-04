

using FluentValidation;

namespace Application.Street.Commands.Update
{
    public class UpdateStreetValidator : AbstractValidator<UpdateStreetCommand>
    {
        public UpdateStreetValidator()
        {
            RuleFor(x => x.StreetName).NotEmpty();
            RuleFor(x => x.StreetLength).NotEmpty();
            RuleFor(x => x.NumberOfHouses).NotEmpty();
            RuleFor(x => x.StreetTypeId).NotEmpty();
            RuleFor(x => x.ResidentialGroupId).NotEmpty();
        }
    }
}

using FluentValidation;

namespace Application.ScheduleTreeTrim.Queries.GetById
{
    public class GetByIdValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdValidator()
        {
            RuleFor(x => x.ScheduleTreeTrimId).NotNull().NotEmpty();
        }
    }
}

using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.CreateInviteCode
{
    public class CreateInviteCodeCommandValidation : AbstractValidator<CreateInviteCodeCommand>
    {
        public CreateInviteCodeCommandValidation()
        {
            RuleFor(cic => cic.CourseId)
                .NotEmpty().WithMessage("The invitecode must belong to a course (missing course id)");
        }
    }
}

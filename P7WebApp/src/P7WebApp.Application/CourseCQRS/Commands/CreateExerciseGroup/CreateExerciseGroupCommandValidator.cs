﻿using FluentValidation;

namespace P7WebApp.Application.CourseCQRS.Commands.CreateExerciseGroup
{
    public class CreateExerciseGroupCommandValidator : AbstractValidator<CreateExerciseGroupCommand>
    {
        public CreateExerciseGroupCommandValidator()
        {
            RuleFor(ceg => ceg.CourseId)
                .NotEmpty().WithMessage("Course id must be provided.")
                .NotNull().WithMessage("Course id cannot be null.")
                .GreaterThan(0).WithMessage("Couse id cannot be negative.");

            RuleFor(ceg => ceg.Title)
                .NotEmpty().WithMessage("Title is required.")
                .NotNull().WithMessage("Title cannot be null.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(ceg => ceg.Description)
                .NotNull().WithMessage("Description cannot be null.");
        }
    }
}

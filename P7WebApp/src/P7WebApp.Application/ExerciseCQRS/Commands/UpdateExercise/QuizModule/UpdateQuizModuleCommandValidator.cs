﻿using FluentValidation;

namespace P7WebApp.Application.ExerciseCQRS.Commands.UpdateExercise.QuizModule
{
    public class UpdateQuizModuleCommandValidator : AbstractValidator<UpdateQuizModuleCommand>
    {
        public UpdateQuizModuleCommandValidator()
        {
            RuleFor(cmc => cmc.Height)
                .NotNull().WithMessage("Must be a valid height.");

            RuleFor(cmc => cmc.Width)
                .NotNull().WithMessage("Must be a valid weidth.");

            RuleFor(cmc => cmc.Position)
                .NotNull().WithMessage("Must be a valid position.");

            RuleFor(cmc => cmc.Description)
                .NotNull().WithMessage("Must be a valid description.");
        }
    }
}
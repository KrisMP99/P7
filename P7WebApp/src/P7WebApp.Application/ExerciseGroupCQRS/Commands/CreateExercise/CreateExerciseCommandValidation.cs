﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise
{
    public class CreateExerciseCommandValidation : AbstractValidator<CreateExerciseCommand>
    {
        public CreateExerciseCommandValidation()
        {
            RuleFor(cec => cec.ExerciseGroupId)
                .NotEmpty().WithMessage("Exercisegroup id must be provided")
                .NotNull().WithMessage("Exercisegroup id cannot be null")
                .GreaterThan(0).WithMessage("Exercisegroup id cannot be negative");

            RuleFor(cec => cec.Title)
                .NotEmpty().WithMessage("Title is required")
                .NotNull().WithMessage("Title cannot be null")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(cec => cec.ExerciseNumber)
                .GreaterThan(0).WithMessage("Exercisenumber cannot be negative");

        }
    }
}

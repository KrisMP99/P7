﻿using MediatR;
using P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise.Module;

namespace P7WebApp.Application.ExerciseGroupCQRS.Commands.CreateExercise
{
    public class CreateExerciseCommand : IRequest<int>
    {
        public CreateExerciseCommand(int exerciseGroupId, string title, bool isVisible, DateTime? startDate, DateTime? endDate, DateTime? visibleFrom, DateTime? visibleTo, int layoutId, List<CreateModuleCommand> modules)
        {
            ExerciseGroupId = exerciseGroupId;
            Title = title;
            IsVisible = isVisible;
            StartDate = startDate;
            EndDate = endDate;
            VisibleFrom = visibleFrom;
            VisibleTo = visibleTo;
            LayoutId = layoutId;
            Modules = modules;

        }

        public int ExerciseGroupId { get; }
        public string Title { get; }
        public bool IsVisible { get; }
        public int ExerciseNumber { get; set; } = 0;
        public DateTime? StartDate { get; }
        public DateTime? EndDate { get; }
        public DateTime? VisibleFrom { get; }
        public DateTime? VisibleTo { get; }
        public int LayoutId { get; }
        public List<CreateModuleCommand>? Modules { get; }
    }
}

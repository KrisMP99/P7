using MediatR;
using P7WebApp.Domain.Aggregates.ExerciseAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseCQRS.Commands.CreateSubmission
{
    public class CreateSubmissionCommand : IRequest<int>
    {
        public CreateSubmissionCommand(int userId, int exerciseId, bool isSubmitted, string title)
        {
            UserId = userId;
            ExerciseId = exerciseId;
            IsSubmitted = isSubmitted;
            Title = title;
            SubmitDate = DateTime.UtcNow;
        }

        public int UserId { get; set; }
        public int ExerciseId { get; private set; }
        public string Title { get; private set; }
        public bool IsSubmitted { get; set; }
        public DateTime? SubmitDate { get; private set; }

    }
}

using MediatR;
namespace P7WebApp.Application.CourseCQRS.Commands
{
    public class CreateExerciseGroupCommand : IRequest<int>
    {
        public CreateExerciseGroupCommand(int courseId, string title, string description, int exerciseGroupNumber, bool isVisible, DateTime? visibleFromDate)
        {
            CourseId = courseId;
            Title = title;
            Description = description;
            ExerciseGroupNumber = exerciseGroupNumber;
            IsVisible = isVisible;
            VisibleFromDate = visibleFromDate;
        }
        public int CourseId { get;  }
        public string Title { get;  }
        public string Description { get;  }
        public int ExerciseGroupNumber { get;  }
        public bool IsVisible { get;  }
        public DateTime? VisibleFromDate { get;}

    }
}

using P7WebApp.SharedKernel;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class CourseOverview :EntityBase
    {
        public CourseOverview(string title, string courseOwner, bool isPrivate, int numberOfExercises, int numberOfAttendees)
        {
            Title = title;
            CourseOwner = courseOwner;
            IsPrivate = isPrivate;
            NumberOfExercises = numberOfExercises;
            NumberOfAttendees = numberOfAttendees;
        }

        public string Title { get; private set; }
        // First name and last name
        public string CourseOwner { get; private set; }
        public bool IsPrivate { get; private set; }
        public int NumberOfExercises { get; private set; }
        public int NumberOfAttendees { get; private set; }
    }
}

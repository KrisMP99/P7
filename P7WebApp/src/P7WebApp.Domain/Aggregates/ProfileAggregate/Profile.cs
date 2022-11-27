using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;

namespace P7WebApp.Domain.Aggregates.ProfileAggregate
{
    public class Profile : EntityBase, IAggregateRoot
    {
        public Profile(string userId, string firstName, string lastName, string email, string userName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
        }

        public string UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string FullName { get => FirstName + " " + LastName; }

        public Course CreateCourse(string title, string description, bool isPrivate)
        {
            // We create a default role that all attendees are assigned to initally when enrolling in the course
            // This could also be done in the command handler, but is here for now.
            var course = new Course(
                ownerId: base.Id,
                title: title,
                description: description,
                isPrivate: isPrivate);

            return course;
        }
    }
}

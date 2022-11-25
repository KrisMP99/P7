using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;
using P7WebApp.Domain.Exceptions;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class Course : AuditableEntityBase, IAggregateRoot
    {
        public Course(string title, string description, bool isPrivate)
        {
            Title = title;
            Description = description;
            IsPrivate = isPrivate;
            ExerciseGroups = new List<ExerciseGroup>();
            CourseRoles = new List<CourseRole>();
            Attendees = new List<Attendee>();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsPrivate { get; private set; }
        public InviteCode? InviteCode { get; private set; }
        public List<ExerciseGroup> ExerciseGroups { get; private set; }
        public List<CourseRole> CourseRoles { get; private set; }
        public List<Attendee> Attendees { get; private set; }
        

        public void EditInformation(string newTitle, string newDescription, bool newVisibility)
        {
            Title = String.IsNullOrEmpty(newTitle) ? throw new CourseException("The title has not been set.") : newTitle;
            Description = String.IsNullOrEmpty(newDescription) ? throw new CourseException("Description has not been set.") : newDescription;
            IsPrivate= newVisibility;
        }

        public ExerciseGroup GetExerciseGroup(int exerciseGroupId)
        {
            try
            {
                var exerciseGroup = ExerciseGroups.Find(e => e.Id == exerciseGroupId);

                if (exerciseGroup is not null)
                {
                    return exerciseGroup;
                }
                else
                {
                    throw new CourseException("Could not find an exercise group with the specified id");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateInviteCode(InviteCode invitecode)
        {
            try
            {
                if(invitecode is not null)
                {
                    InviteCode = invitecode;
                }
                else
                {
                    throw new CourseException("Could not create the invite code");
                }    
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void RemoveInviteCode()
        {
            throw new NotImplementedException();
        }

        public void AddAttendee(Attendee attendee)
        {
            try
            {
                if(attendee is not null)
                {
                    Attendees.Add(attendee);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void RemoveAttendee(string userId)
        {
            try
            {
                var attendee = Attendees.Where(a => a.UserId == userId).FirstOrDefault();
                if (attendee is not null)
                {
                    Attendees.Remove(attendee);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddExerciseGroup(ExerciseGroup exerciseGroup)
        {
            try
            {
                if(exerciseGroup is null)
                {
                    throw new CourseException("Could not add the exercisegroup to the course (exercisegroup is null)");
                }
                
                if(CheckExerciseGroupNumberIsOk(exerciseGroup))
                {
                    ExerciseGroups.Add(exerciseGroup);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private bool CheckExerciseGroupNumberIsOk(ExerciseGroup exerciseGroup)
        {
            if(exerciseGroup.ExerciseGroupNumber < 0)
            {
                throw new CourseException("The exercise group number cannot be less than 0.");
            }

            var result = ExerciseGroups.Find(eg => eg.Id == exerciseGroup.ExerciseGroupNumber);

            if (result is not null)
            {
                throw new CourseException($"An exercise group with exercise group number {result.ExerciseGroupNumber} already exists.");
            }

            return true;
        }

        public void RemoveExerciseGroup(int exerciseGroupId)
        {
            try
            {
                ExerciseGroups.Remove(GetExerciseGroup(exerciseGroupId));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

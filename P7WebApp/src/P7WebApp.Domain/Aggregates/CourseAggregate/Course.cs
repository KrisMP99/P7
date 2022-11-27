using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.Domain.Aggregates.ProfileAggregate;
using P7WebApp.Domain.Common;
using P7WebApp.Domain.Common.Interfaces;
using P7WebApp.Domain.Exceptions;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class Course : EntityBase, IAggregateRoot
    {
        // Private constructor only used by EF core
        private Course() { }
        public Course(int ownerId, string title, string description, bool isPrivate, CourseRole? defaultRole = null)
        {
            Title = title;
            Description = description;
            IsPrivate = isPrivate;
            CreatedDate = DateTime.UtcNow;
            LastModifiedDate = CreatedDate;
            OwnerId = ownerId;
            LastModifiedById = ownerId;
            CourseRoles.Add(CourseRole.CreateDefaultCourseRole(base.Id));
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsPrivate { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public int OwnerId { get; private set; }
        public Profile Owner { get; private set; }
        public int LastModifiedById { get; private set; }
        public Profile LastModifiedBy { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public InviteCode? InviteCode { get; private set; }
        public ICollection<ExerciseGroup> ExerciseGroups { get; private set; } = new List<ExerciseGroup>();
        public ICollection<CourseRole> CourseRoles { get; private set; } = new List<CourseRole>();
        public ICollection<Attendee> Attendees { get; private set; } = new List<Attendee>();

        public void EditInformation(string newTitle, string newDescription, bool newVisibility)
        {
            Title = String.IsNullOrEmpty(newTitle) ? throw new CourseException("The title has not been set.") : newTitle;
            Description = String.IsNullOrEmpty(newDescription) ? throw new CourseException("Description has not been set.") : newDescription;
            IsPrivate= newVisibility;
            LastModifiedDate = DateTime.UtcNow;
        }

        public ExerciseGroup GetExerciseGroup(int exerciseGroupId)
        {
            try
            {
                var exerciseGroup = ExerciseGroups.FirstOrDefault(eg => eg.Id == groupId);

                if (exerciseGroup is null)
                {
                    throw new Exception("Could not find an exerciseGroup with the specified Id");
                }

                return exerciseGroup;
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
                if(invitecode is null)
                {
                    throw new Exception("Could not create the invite code");             
                }
                InviteCode = invitecode;
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
                if(attendee is null)
                {
                    throw new Exception("Attendee list has not been initialized.");
                }

                Attendees.Add(attendee);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Attendee GetAttendeeByProfileId(int profileId)
        {
            var attendee = Attendees.FirstOrDefault(e => e.ProfileId == profileId);

            if (attendee is null)
            {
                throw new Exception("Could not find attendee with the given profile id.");
            }

            return attendee;
        }

        public void RemoveAttendeeByProfileId(int profileId)
        {
            try
            {
                Attendees.Remove(GetAttendeeByProfileId(profileId));
            }
            catch(Exception)
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
                    throw new Exception("Could not add the exercisegroup to the course (exercisegroup is null)");    
                }
                ExerciseGroups.Add(exerciseGroup);
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

        public void AddCourseRole(CourseRole courseRole)
        {
            try
            {
                CourseRoles.Add(courseRole);
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}

using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;
using P7WebApp.SharedKernel;
using P7WebApp.SharedKernel.Interfaces;

namespace P7WebApp.Domain.Aggregates.CourseAggregate
{
    public class Course : EntityBase, IAggregateRoot
    {
        public int CourseId { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsPrivate { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public int OwnerId { get; private set; }
        public InviteCode? InviteCode { get; private set; }
        public List<ExerciseGroup> ExerciseGroups { get; private set; }
        public List<CourseRole> CourseRoles { get; private set; }

        public void EditInformation(string newTitle, string newDescription, bool newVisibility)
        {
            throw new NotImplementedException();
        }

        public ExerciseGroup GetExerciseGroup(int groupId)
        {
            try
            {
                var exerciseGroup =  ExerciseGroups.Find(e => e.Id == groupId);

                if (exerciseGroup is not null)
                {
                    return exerciseGroup;
                }
                else
                {
                    throw new Exception("Could not find an exercise with the specified Id");
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
                    throw new Exception("Could not create the invite code");
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

        public void AddAttendee()
        {
            throw new NotImplementedException();
        }

        public void RemoveAttendee()
        {
            throw new NotImplementedException();
        }

        public void AddExerciseGroup(ExerciseGroup exerciseGroup)
        {
            try
            {
                if(exerciseGroup is not null)
                {
                    ExerciseGroups.Add(exerciseGroup);
                }
                else
                {
                    throw new Exception("Could not add the exercisegroup to the course (exercisegroup is null)");
                }
            }
            catch(Exception)
            {
                throw;
            }
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

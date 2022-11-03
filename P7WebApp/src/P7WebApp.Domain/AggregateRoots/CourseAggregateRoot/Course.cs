using P7WebApp.SharedKernel;
using P7WebApp.SharedKernel.Interfaces;

namespace P7WebApp.Domain.AggregateRoots.CourseAggregateRoot
{
    public class Course : EntityBase, IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int OwnerId { get; set; }
        public InviteCode? InviteCode { get; set; }
        public List<ExerciseGroup> ExerciseGroups { get; set; }

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

        public void AddExerciseGroup(ExerciseGroup exercisegroup)
        {
            try
            {
                if(exercisegroup is not null)
                {
                    ExerciseGroups.Add(exercisegroup);
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
    }
}

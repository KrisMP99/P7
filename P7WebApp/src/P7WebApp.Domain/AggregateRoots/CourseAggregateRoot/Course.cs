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
        public List<ExerciseGroup> ExerciseGroups { get; set; }

        public void UpdateInformation(string newTitle, string newDescription, bool newVisibility)
        {
            Title = string.IsNullOrEmpty(newTitle) ? Title : newTitle;
            Description = string.IsNullOrEmpty(newDescription) ? Description : Description == newDescription ? Description : newDescription;
            IsPrivate = IsPrivate && newVisibility ? IsPrivate : newVisibility;
        }

        public ExerciseGroup GetExerciseGroup(int groupId)
        {
            try
            {
                if (ExerciseGroups.Any(e => e.Id == groupId))
                {
                    return ExerciseGroups.Find(e => e.Id == groupId);
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
    }
}

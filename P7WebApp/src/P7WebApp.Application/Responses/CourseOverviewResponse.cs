namespace P7WebApp.Application.Responses
{
    public class CourseOverviewResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string OwnerName { get; set; }
        public int NumberOfAtteendes { get; set; }
        public int NumberOfExercies { get; set; }
    }
}

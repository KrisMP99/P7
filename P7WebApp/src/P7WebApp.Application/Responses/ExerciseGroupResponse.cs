﻿namespace P7WebApp.Application.Responses
{
    public class ExerciseGroupResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public int ExerciseGroupNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime BecomeVisibleAt { get; set; }
        public IEnumerable<ExerciseOverviewResponse> Exercises { get; set; }
    }
}

using MediatR;
using P7WebApp.Application.Responses;

namespace P7WebApp.Application.CourseCQRS.Queries
{
    public class GetOwnedCoursesQuery : IRequest<IEnumerable<CourseOverviewResponse>>
    {
        public GetOwnedCoursesQuery(string userId)
        {
            this.userId = userId;
        }

        public string userId { get; set; }
    }
}

using MediatR;
using P7WebApp.Application.Responses;

namespace P7WebApp.Application.CourseCQRS.Queries
{
    public class GetCreatedCoursesQuery : IRequest<IEnumerable<CourseOverviewResponse>>
    {
    }
}

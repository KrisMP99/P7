using MediatR;
using P7WebApp.Application.Responses;

namespace P7WebApp.Application.ProfileCQRS.Queries
{
    public class GetProfileAttendedCoursesQuery : IRequest<IEnumerable<CourseOverviewResponse>>
    {
    }
}

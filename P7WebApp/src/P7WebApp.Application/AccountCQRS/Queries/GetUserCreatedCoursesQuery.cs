using MediatR;
using P7WebApp.Application.Responses;

namespace P7WebApp.Application.UserCQRS.Queries
{
    public class GetUserCreatedCoursesQuery : IRequest<IEnumerable<CourseOverviewResponse>>
    {

    }
}

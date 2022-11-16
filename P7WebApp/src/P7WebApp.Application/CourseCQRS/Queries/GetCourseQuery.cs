using MediatR;
using P7WebApp.Application.Responses;

namespace P7WebApp.Application.CourseCQRS.Queries
{
    public class GetCourseQuery: IRequest<CourseResponse>
    {
        public GetCourseQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; } 
    }
}

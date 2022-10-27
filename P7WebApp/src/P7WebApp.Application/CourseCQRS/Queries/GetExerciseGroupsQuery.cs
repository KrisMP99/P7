using MediatR;
using P7WebApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.CourseCQRS.Queries
{
    public class GetExerciseGroupsQuery : IRequest<IEnumerable<ExerciseGroupsResponse>>
    {
        public GetExerciseGroupsQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

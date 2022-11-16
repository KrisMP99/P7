using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7WebApp.Application.ExerciseQCRS.Commands
{
    public class DeleteSubmissionCommand : IRequest<int>
    {
        public int SubmissionId { get; set; }
    }
}

using MediatR;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Application.Common.Mappings;
using P7WebApp.Application.CourseCQRS.Commands.DeleteCourse;
using P7WebApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P7WebApp.Application.CourseCQRS.Commands;
using P7WebApp.Application.Common.Exceptions;

namespace P7WebApp.Application.CourseCQRS.CommandHandlers
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.CourseRepository.DeleteCourse(request.Id);
                int rowsAffected = await _unitOfWork.CommitChangesAsync(cancellationToken);

                if (rowsAffected == 0)
                {
                    throw new NotFoundException($"Could not find a course witht the specified Id {request.Id}");
                }

                return rowsAffected;
            }
            catch (Exception)
            { 
                throw new Exception($"Could not delete course with Id: {request.Id}.");
            }
        }
    }
}
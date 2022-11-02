﻿using P7WebApp.Domain.Repositories;

namespace P7WebApp.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        Task<int> CommitChangesAsync(CancellationToken cancellationToken);
    }
}

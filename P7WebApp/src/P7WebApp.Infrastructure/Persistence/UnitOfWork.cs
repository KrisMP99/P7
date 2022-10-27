﻿
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Repositories;
using P7WebApp.Infrastructure.Repositories;

namespace P7WebApp.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IApplicationDbContext _context;

        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            CourseRepository = new CourseRepository(context);
        }
        public ICourseRepository CourseRepository { get; private set; }


        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
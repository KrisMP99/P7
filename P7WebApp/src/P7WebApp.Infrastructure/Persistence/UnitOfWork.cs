using Microsoft.Extensions.Logging;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Repositories;
using P7WebApp.Infrastructure.Repositories;

namespace P7WebApp.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private bool _disposed = false;

        public UnitOfWork(IApplicationDbContext context, ILogger<UnitOfWork> logger, ILogger<CourseRepository> courseLogger, ILogger<ExerciseGroupRepository> exerciseGroupLogger, ILogger<ExerciseRepository> exerciseLogger, ILogger<ProfileRepository> profileLogger)
        {
            _context = context;
            _logger = logger;
            CourseRepository = new CourseRepository(context, courseLogger);
            ExerciseGroupRepository = new ExerciseGroupRepository(context, exerciseGroupLogger);
            ExerciseRepository = new ExerciseRepository(context, exerciseLogger);
            ProfileRepository = new ProfileRepository(context, profileLogger);
        }

        public ICourseRepository CourseRepository { get; private set; }
        public IExerciseGroupRepository ExerciseGroupRepository { get; private set; }
        public IExerciseRepository ExerciseRepository { get; private set; }
        public IProfileRepository ProfileRepository { get; private set; }

        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("trying to commit changes");
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error orcurred while commiting to db, with message {ex.Message}");
                throw;
            }
        }

    }
}

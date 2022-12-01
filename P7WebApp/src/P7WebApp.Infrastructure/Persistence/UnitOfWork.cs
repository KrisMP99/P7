using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Domain.Repositories;
using P7WebApp.Infrastructure.Repositories;

namespace P7WebApp.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IApplicationDbContext _context;
        private bool _disposed = false;

        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            CourseRepository = new CourseRepository(context);
            ExerciseGroupRepository = new ExerciseGroupRepository(context);
            ExerciseRepository = new ExerciseRepository(context);
            ProfileRepository = new ProfileRepository(context);
        }

        public ICourseRepository CourseRepository { get; private set; }
        public IExerciseGroupRepository ExerciseGroupRepository { get; private set; }
        public IExerciseRepository ExerciseRepository { get; private set; }
        public IProfileRepository ProfileRepository { get; private set; }

        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual async ValueTask DisposeAsync(bool disposing) 
        {
            if(!this._disposed)
            {
                if(disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            this._disposed= true;
        }

        public async void Dispose()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
    }
}

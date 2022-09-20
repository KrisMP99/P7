using MediatR;
using Microsoft.EntityFrameworkCore;
using P7WebApp.Application.Common.Interfaces;
using P7WebApp.Infrastructure.Common;

namespace P7WebApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private IMediator _mediator;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

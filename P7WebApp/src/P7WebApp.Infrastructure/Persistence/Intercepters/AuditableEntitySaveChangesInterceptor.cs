//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using Microsoft.VisualStudio.Services.Users;
//using P7WebApp.Application.Common.Interfaces;
//using P7WebApp.Domain.Common;
//using P7WebApp.Domain.Identity;
//using System.Security.Claims;

//namespace P7WebApp.Infrastructure.Persistence.Intercepters
//{
//    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor, IAuditableEntitySaveChangesInterceptor
//    {
//        private readonly ICurrentUserService _currentUserService;
//        private readonly IDateTime _dateTime;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public AuditableEntitySaveChangesInterceptor(
//            ICurrentUserService currentUserService,
//            IDateTime dateTime,
//            UserManager<ApplicationUser> userManager,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _currentUserService = currentUserService;
//            _dateTime = dateTime;
//            _userManager = userManager;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        //public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
//        //{
//        //    UpdateEntities(eventData.Context);

//        //    return base.SavingChanges(eventData, result);
//        //}

//        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
//        {
//            UpdateEntities(eventData.Context);

//            return await base.SavingChangesAsync(eventData, result, cancellationToken);
//        }

//        public async void UpdateEntities(DbContext? context)
//        {
//            if (context == null) return;

//            var user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

//            var user  _currentUserService.UserId;

//            foreach (var entry in context.ChangeTracker.Entries<AuditableEntityBase>())
//            {
//                if (entry.State == EntityState.Added)
//                {
//                    entry.Entity.CreatedById = user.Id;
//                    entry.Entity.CreatedBy = user;
//                    entry.Entity.CreatedDate = _dateTime.Now;
//                }

//                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
//                {
//                    entry.Entity.LastModifiedById = user.Id;
//                    entry.Entity.LastModifiedBy = user;
//                    entry.Entity.ModifiedDate = _dateTime.Now;
//                }
//            }
//        }
//    }

//    public static class Extensions
//    {
//        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
//            entry.References.Any(r =>
//                r.TargetEntry != null &&
//                r.TargetEntry.Metadata.IsOwned() &&
//                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
//    }
//}

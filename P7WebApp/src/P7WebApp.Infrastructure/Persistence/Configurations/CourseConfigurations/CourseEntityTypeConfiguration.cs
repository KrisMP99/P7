using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P7WebApp.Domain.Aggregates.CourseAggregate;
using P7WebApp.Infrastructure.Identity;

namespace P7WebApp.Infrastructure.Persistence.Configurations.CourseConfigurations
{
    public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses").HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
            builder.Property(x => x.IsPrivate).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.Property(x => x.OwnerId).IsRequired();

            builder.OwnsOne(c => c.InviteCode, iv =>
            {
                iv.WithOwner().HasForeignKey(ic => ic.CourseId);
                iv.ToTable("InviteCodes").HasKey(ic => ic.Id);

                iv.Property(ic => ic.Id).UseIdentityColumn().IsRequired();
                iv.Property(ic => ic.CourseId).IsRequired();
                iv.Property(ic => ic.Code).IsRequired();
                iv.Property(ic => ic.IsActive).IsRequired();
                iv.Property(ic => ic.UseableFrom).IsRequired();
                iv.Property(ic => ic.UseableTo).IsRequired();
            });
            builder.OwnsMany(c => c.CourseRoles, cr =>
            {
                cr.ToTable("CourseRoles").HasKey(cr => cr.Id);

                cr.Property(cr => cr.Id).UseIdentityColumn().IsRequired();
                cr.Property(cr => cr.RoleName).HasMaxLength(50).IsRequired();
                cr.Property(cr => cr.CourseId).IsRequired();

                cr.OwnsOne(cr => cr.Permisson, p =>
                {
                    p.WithOwner().HasForeignKey(p => p.CourseRoleId);

                    p.ToTable("Permissions").HasKey(p => p.Id);
                    p.Property(p => p.Id).UseIdentityColumn().IsRequired();
                    p.Property(p => p.CourseRoleId).IsRequired();
                    p.Property(p => p.CanAddExerciseGroup).IsRequired();
                    p.Property(p => p.CanAddExercise).IsRequired();
                    p.Property(p => p.CanCreateRoles).IsRequired();
                    p.Property(p => p.CanCreateIniviteCode).IsRequired();
                    p.Property(p => p.CanRevokeInviteCode).IsRequired();
                    p.Property(p => p.CanDeleteExerciseGroup).IsRequired();
                    p.Property(p => p.CanDeleteExercise).IsRequired();
                    p.Property(p => p.CanDeleteSubmission).IsRequired();
                    p.Property(p => p.CanUpdateCourse).IsRequired();
                    p.Property(p => p.CanUpdateExercise).IsRequired();
                    p.Property(p => p.CanViewSubmission).IsRequired();
                });
            });
            builder.OwnsMany(c => c.ExerciseGroups, eg =>
                {
                    eg.WithOwner().HasForeignKey(eg => eg.CourseId);
                    eg.ToTable("ExerciseGroups").HasKey(eg => eg.Id);

                    eg.Property(eg => eg.Id).UseIdentityColumn().IsRequired();
                    eg.Property(eg => eg.Title).IsRequired();
                    eg.Property(eg => eg.Description).IsRequired();
                    eg.Property(eg => eg.ExerciseGroupNumber).IsRequired();
                    eg.Property(eg => eg.CreatedDate).IsRequired();
                    eg.Property(eg => eg.LastModifiedDate).IsRequired();
                    eg.Property(eg => eg.IsVisible).IsRequired();
                    eg.Property(eg => eg.VisibleFromDate).IsRequired();

                    eg.OwnsMany(eg => eg.Exercises, e =>
                    {
                        e.WithOwner().HasForeignKey(e => e.ExerciseGroupId);
                        e.ToTable("Exercises").HasKey(e => e.Id);

                        e.Property(e => e.Id).UseIdentityColumn().IsRequired();
                        e.Property(e => e.ExerciseGroupId).IsRequired();
                        e.Property(e => e.Title).HasMaxLength(50).IsRequired();
                        e.Property(e => e.ExerciseNumber).IsRequired();
                        e.Property(e => e.StartDate);
                        e.Property(e => e.EndDate).IsRequired();
                        e.Property(e => e.CreatedDate).IsRequired();
                        e.Property(e => e.LastModifiedDate).IsRequired();

                        e.OwnsMany(e => e.Modules).WithOwner().HasForeignKey(m => m.BelongsToId);
                        e.OwnsMany(e => e.Submissions, s =>
                        {
                            s.WithOwner().HasForeignKey(s => s.ExerciseId);
                            s.ToTable("Submissions").HasKey(s => s.Id);

                            s.Property(s => s.Id).UseIdentityColumn().IsRequired();
                            s.Property(s => s.Title).HasMaxLength(100).IsRequired();
                            s.Property(s => s.SubmitDate).IsRequired();

                            s.OwnsMany(s => s.Modules).WithOwner().HasForeignKey(m => m.BelongsToId);

                        });
                    });

                });
        }
    }
}
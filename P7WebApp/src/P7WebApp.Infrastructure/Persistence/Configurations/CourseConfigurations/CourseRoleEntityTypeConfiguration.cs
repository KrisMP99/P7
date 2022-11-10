//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using P7WebApp.Domain.Aggregates.CourseAggregate;

//namespace P7WebApp.Infrastructure.Persistence.Configurations.CourseConfigurations
//{
//    public class CourseRoleEntityTypeConfiguration : IEntityTypeConfiguration<CourseRole>
//    {
//        public void Configure(EntityTypeBuilder<CourseRole> builder)
//        {
//            builder.ToTable("CourseRoles").HasKey(cr => cr.Id);

//            builder.Property(cr => cr.Id).UseIdentityColumn().IsRequired();
//            builder.Property(cr => cr.RoleName).HasMaxLength(50).IsRequired();
//            builder.Property(cr => cr.CourseId).IsRequired();

//            builder.OwnsOne(cr => cr.Permisson).WithOwner().HasForeignKey(p => p.CourseRoleId);
//        }
//    }
//}
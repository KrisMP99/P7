//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using P7WebApp.Domain.Aggregates.ExerciseGroupAggregate;

//namespace P7WebApp.Infrastructure.Persistence.Configurations.CourseConfigurations
//{
//    public class ExerciseGroupEntityTypeConfiguration : IEntityTypeConfiguration<ExerciseGroup>
//    {
//        public void Configure(EntityTypeBuilder<ExerciseGroup> builder)
//        {
//            builder.ToTable("ExerciseGroups").HasKey(eg => eg.Id);

//            builder.Property(eg => eg.Id).UseIdentityColumn().IsRequired();
//            builder.Property(eg => eg.Title).IsRequired();
//            builder.Property(eg => eg.Description).IsRequired();
//            builder.Property(eg => eg.ExerciseGroupNumber).IsRequired();
//            builder.Property(eg => eg.CreatedDate).IsRequired();
//            builder.Property(eg => eg.LastModifiedDate).IsRequired();
//            builder.Property(eg => eg.IsVisible).IsRequired();
//            builder.Property(eg => eg.VisibleFromDate).IsRequired();

//            builder.OwnsMany(eg => eg.Exercises).WithOwner().HasForeignKey(e => e.ExerciseGroupId);
//        }
//    }
//}
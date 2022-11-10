//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using P7WebApp.Domain.Aggregates.ExerciseAggregate;

//namespace P7WebApp.Infrastructure.Persistence.Configurations.ExerciseConfigurations
//{
//    public class ExerciseEntityTypeConfiguration : IEntityTypeConfiguration<Exercise>
//    {
//        public void Configure(EntityTypeBuilder<Exercise> builder)
//        {
//            builder.ToTable("Exercises").HasKey(e => e.Id);

//            builder.Property(e => e.Id).UseIdentityColumn().IsRequired();
//            builder.Property(e => e.ExerciseGroupId).IsRequired();
//            builder.Property(e => e.Title).HasMaxLength(50).IsRequired();
//            builder.Property(e => e.ExerciseNumber).IsRequired();
//            builder.Property(e => e.StartDate);
//            builder.Property(e => e.EndDate).IsRequired();
//            builder.Property(e => e.CreatedDate).IsRequired();
//            builder.Property(e => e.LastModifiedDate).IsRequired();

//            builder.OwnsMany(e => e.Modules).WithOwner().HasForeignKey(m => m.BelongsToId);
//            builder.OwnsMany(e => e.Submissions).WithOwner().HasForeignKey(s => s.ExerciseId);
//        }
//    }
//}

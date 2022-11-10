//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using P7WebApp.Domain.Aggregates.ExerciseAggregate;

//namespace P7WebApp.Infrastructure.Persistence.Configurations.ExerciseConfigurations
//{
//    public class SolutionEntityTypeConfiguration : IEntityTypeConfiguration<Solution>
//    {
//        public void Configure(EntityTypeBuilder<Solution> builder)
//        {
//            builder.ToTable("Solutions").HasKey(s => s.Id);

//            builder.Property(s => s.Id).UseIdentityColumn().IsRequired();
//            builder.Property(s => s.ExerciseId).IsRequired();
//            builder.Property(s => s.IsVisible).IsRequired();
//            builder.Property(s => s.VisibleFromDate).IsRequired();

//            builder.OwnsMany(s => s.Modules).WithOwner().HasForeignKey(m => m.BelongsToId);
//        }
//    }
//}
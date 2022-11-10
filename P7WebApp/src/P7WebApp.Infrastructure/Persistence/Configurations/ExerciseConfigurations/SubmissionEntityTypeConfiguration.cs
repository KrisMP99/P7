//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using P7WebApp.Domain.Aggregates.ExerciseAggregate;

//namespace P7WebApp.Infrastructure.Persistence.Configurations.ExerciseConfigurations
//{
    //public class SubmissionEntityTypeConfiguration : IEntityTypeConfiguration<Submission>
    //{
    //    public void Configure(EntityTypeBuilder<Submission> builder)
    //    {
    //        builder.ToTable("Submissions").HasKey(s => s.Id);

    //        builder.Property(s => s.Id).UseIdentityColumn().IsRequired();
    //        builder.Property(s => s.Title).HasMaxLength(100).IsRequired();
    //        builder.Property(s => s.SubmitDate).IsRequired();

    //        builder.OwnsMany(s => s.Modules).WithOwner().HasForeignKey(m => m.BelongsToId);
    //    }
    //}
//}
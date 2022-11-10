//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using P7WebApp.Domain.Aggregates.ExerciseAggregate;

//namespace P7WebApp.Infrastructure.Persistence.Configurations.ExerciseConfigurations
//{
//    public class SubmissionDraftEntityTypeConfigurations : IEntityTypeConfiguration<SubmissionDraft>
//    {
//        public void Configure(EntityTypeBuilder<SubmissionDraft> builder)
//        {
//            builder.ToTable("SubmissionDrafts").HasKey(sd => sd.Id);

//            builder.Property(sd => sd.Id).UseIdentityColumn().IsRequired();
//            builder.Property(sd => sd.Title).HasMaxLength(100).IsRequired();

//            builder.OwnsMany(sd => sd.Modules).WithOwner().HasForeignKey(m => m.BelongsToId);
//            builder.OwnsMany(sd => sd.Submissions).WithOwner().HasForeignKey(s => s.SubmissionDraftId);
//        }
//    }
//}

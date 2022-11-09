//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.QuizModule;

//namespace P7WebApp.Infrastructure.Persistence.Configurations.ModuleConfiguration
//{
//    public class QuizModuleEntityTypeConfiguration : ModuleConfiguration<QuizModule>
//    {
//        public override void Configure(EntityTypeBuilder<QuizModule> builder)
//        {
//            base.Configure(builder);

//            //builder.HasKey(m => m.Id);
//            //builder.Property(m => m.Id).UseIdentityColumn().IsRequired();

//            builder.OwnsMany(qm => qm.Questions, q =>
//            {
//                q.WithOwner().HasForeignKey(q => q.QuizModuleId);
//                q.ToTable("Questions").HasKey(q => q.Id);

//                q.Property(q => q.Text).HasMaxLength(500).IsRequired();
//                q.Property(q => q.QuizModuleId).IsRequired();

//                q.OwnsMany(q => q.Choices, c =>
//                {
//                    c.WithOwner().HasForeignKey(c => c.QuestionId);
//                    c.ToTable("Choices").HasKey(q => q.Id);

//                    c.Property(c => c.Text).HasMaxLength(500).IsRequired();
//                    c.Property(c => c.QuestionId).IsRequired();
//                    c.Property(c => c.IsCorrect).IsRequired();
//                });
//            });
//        }
//    }
//}
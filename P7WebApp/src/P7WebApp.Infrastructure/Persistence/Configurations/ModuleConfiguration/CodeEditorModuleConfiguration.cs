using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.CodeModule;

namespace P7WebApp.Infrastructure.Persistence.Configurations.ModuleConfiguration
{
    public class CodeEditorModuleConfiguration : ModuleConfiguration<CodeEditorModule>
    {
        public override void Configure(EntityTypeBuilder<CodeEditorModule> builder)
        {
            base.Configure(builder);
            //builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).UseIdentityColumn().IsRequired();

            builder.ToTable("CodeEditorModules");

            builder.Property(cem => cem.Code).IsRequired();

            builder.OwnsMany(cem => cem.TestCases, tc =>
            {
                tc.WithOwner().HasForeignKey(tc => tc.CodeEditorModuleId);
                tc.ToTable("TestCases").HasKey(tc => tc.Id);

                tc.Property(tc => tc.Id).UseIdentityColumn().IsRequired();
                tc.Property(tc => tc.CodeEditorModuleId).IsRequired();
                tc.Property(tc => tc.Test).IsRequired();
            });
        }
    }
}
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules.TextModule;

//namespace P7WebApp.Infrastructure.Persistence.Configurations.ModuleConfiguration
//{
//    public class TextModuleEntityTypeConfiguration : ModuleConfiguration<TextModule>
//    {
//        public override void Configure(EntityTypeBuilder<TextModule> builder)
//        {
//            base.Configure(builder);
//            //builder.HasKey(m => m.Id);
//            //builder.Property(m => m.Id).UseIdentityColumn().IsRequired();

//            builder.Property(tm => tm.Text).IsRequired();

//            builder.OwnsMany(tm => tm.Images, i =>
//            {
//                i.WithOwner().HasForeignKey(c => c.TextModuleId);

//                i.ToTable("Images").HasKey(i => i.Id);

//                i.Property(i => i.Id).UseIdentityColumn().IsRequired();
//                i.Property(i => i.File).IsRequired();
//            });
//        }
//    }
//}
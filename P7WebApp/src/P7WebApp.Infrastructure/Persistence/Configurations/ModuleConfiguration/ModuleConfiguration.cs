//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using P7WebApp.Domain.Aggregates.ExerciseAggregate.Modules;

//namespace P7WebApp.Infrastructure.Persistence.Configurations.ModuleConfiguration
//{
//    public class ModuleConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Module
//    {
//        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
//        {
//            builder.HasKey(m => m.Id);
//            builder.Property(m => m.BelongsToId).IsRequired();
//            builder.Property(m => m.Description).HasMaxLength(500).IsRequired();
//            builder.Property(m => m.Height).IsRequired();
//            builder.Property(m => m.Width).IsRequired();
//            builder.Property(m => m.Posititon).IsRequired();
//        }
//    }
//}
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using P7WebApp.Domain.Aggregates.CourseAggregate;

//namespace P7WebApp.Infrastructure.Persistence.Configurations.CourseConfigurations
//{
//    public class InviteCodeEntityTypeConfiguration : IEntityTypeConfiguration<InviteCode>
//    {
//        public void Configure(EntityTypeBuilder<InviteCode> builder)
//        {
//            builder.ToTable("InviteCodes").HasKey(ic => ic.Id);

//            builder.Property(ic => ic.Id).UseIdentityColumn().IsRequired();
//            builder.Property(ic => ic.CourseId).IsRequired();
//            builder.Property(ic => ic.Code).IsRequired();
//            builder.Property(ic => ic.IsActive).IsRequired();
//            builder.Property(ic => ic.UseableFrom).IsRequired();
//            builder.Property(ic => ic.UseableTo).IsRequired();
//        }
//    }
//}
using P7WebApp.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.Domain.Common
{
    public class AuditableEntityBase : EntityBase
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public string? LastModifiedById { get; set; }
        public ApplicationUser? LastModifiedBy { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.SharedKernel
{
    public class AuditableEntityBase : EntityBase
    {
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}

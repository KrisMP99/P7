using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.SharedKernel.Interfaces
{
    public class AuditableEntityBase : EntityBase
    {
        public DateTime CreatedDate { get; set; }

        [ForeignKey("AspNetUsers")]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("AspNetUsers")]
        public string? LastModifiedBy { get; set; }
    }
}

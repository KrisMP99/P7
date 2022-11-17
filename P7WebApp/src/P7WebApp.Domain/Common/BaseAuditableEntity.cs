﻿using P7WebApp.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P7WebApp.Domain.Common
{
    public class AuditableEntityBase : EntityBase
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; }

        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
        public string? LastModifiedById { get; set; }
    }
}

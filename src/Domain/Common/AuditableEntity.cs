using System;

namespace CleanArchitecture.Domain.Common
{
    public abstract class AuditableEntity
    {
        public string CreatedByUserId { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public string LastModifiedByUserId { get; set; }

        public DateTime? LastModifiedOnDate { get; set; }
    }
}

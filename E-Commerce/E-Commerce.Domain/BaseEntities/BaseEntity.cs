using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.BaseEntities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool Active { get; set; } = true;
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation properties
        public string? CreatedBy { get; set; }
        public ApplicationUser? CreatedByUser { get; set; }

        public string? UpdatedBy { get; set; }
        public ApplicationUser? UpdatedByUser { get; set; }

        public string? DeletedBy { get; set; }
        public ApplicationUser? DeletedByUser { get; set; }
    }
}

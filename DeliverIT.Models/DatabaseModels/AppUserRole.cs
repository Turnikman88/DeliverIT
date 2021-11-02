using DeliverIT.Models.Contracts;
using System;

namespace DeliverIT.Models.DatabaseModels
{
    public class AppUserRole : IDeletable
    {
        public int AppRoleId { get; set; }
        public virtual AppRole AppRole { get; set; }
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}

namespace DeliverIT.Models.DatabaseModels
{
    public class AppUserRole
    {
        public int AppRoleId { get; set; }
        public virtual AppRole AppRole { get; set; }
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}

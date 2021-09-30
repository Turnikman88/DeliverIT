using DeliverIT.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DeliverIT.Models
{
    public partial class DeliverITDBContext : DbContext
    {
        public DeliverITDBContext()
        {
        }

        public DeliverITDBContext(DbContextOptions<DeliverITDBContext> options)
            : base(options)
        {
        }        

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Parcel> Parcels { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<WareHouse> WareHouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

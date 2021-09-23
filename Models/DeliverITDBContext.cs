using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Models
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Integrated Security=true;Database=DeliverIT");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasIndex(e => e.CityId, "IX_Addresses_CityId");

                entity.Property(e => e.StreetName).IsRequired();

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => e.CountryId, "IX_Cities_CountryId");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.AddressId, "IX_Customers_AddressId");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AddressId);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.AddressId, "IX_Employees_AddressId");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AddressId);
            });

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "IX_Parcels_CategoryId");

                entity.HasIndex(e => e.CustomerId, "IX_Parcels_CustomerId");

                entity.HasIndex(e => e.WareHouseId, "IX_Parcels_WareHouseId");

                entity.HasIndex(e => e.ShipmentId, "IX_Parcels_ShipmentId");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.WareHouse)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.WareHouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.Parcels)
                    .HasForeignKey(d => d.ShipmentId);

            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasIndex(e => e.DestinationWareHouseId, "IX_Shipments_DestinationWareHouseId");

                entity.HasIndex(e => e.OriginWareHouseId, "IX_Shipments_OriginWareHouseId");

                entity.HasIndex(e => e.StatusId, "IX_Shipments_StatusId");

                entity.HasOne(d => d.DestinationWareHouse)
                    .WithMany(p => p.ShipmentDestinationWareHouses)
                    .HasForeignKey(d => d.DestinationWareHouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.OriginWareHouse)
                    .WithMany(p => p.ShipmentOriginWareHouses)
                    .HasForeignKey(d => d.OriginWareHouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<WareHouse>(entity =>
            {
                entity.HasIndex(e => e.AddressId, "IX_WareHouses_AddressId");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.WareHouses)
                    .HasForeignKey(d => d.AddressId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

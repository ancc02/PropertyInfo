using PropertyInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace PropertyInfo.API.DbContexts
{
    /// <summary>
    /// Class PropertyInfoContext - Entity framework context
    /// </summary>
    public class PropertyInfoContext : DbContext
    {
        /// <summary>
        /// Table Owners
        /// </summary>
        public DbSet<Owner> Owners { get; set; }
        /// <summary>
        /// Table Property
        /// </summary>
        public DbSet<Property> Properties { get; set; }
        /// <summary>
        /// Table Property images
        /// </summary>
        public DbSet<PropertyImage> PropertyImages { get; set; }
        /// <summary>
        /// Table Property trace
        /// </summary>
        public DbSet<PropertyTrace> PropertyTraces { get; set; }

        /// <summary>
        /// Constructor class PropertyInfoContext
        /// </summary>
        /// <param name="options">configuration for Object Relational Mapping (ORM)</param>
        public PropertyInfoContext(DbContextOptions<PropertyInfoContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Model create
        /// </summary>
        /// <param name="modelBuilder">object to create model</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
                 .HasData(
                new Owner()
                {
                    IdOwner = 1,
                    Name = "Name owner first",
                    Address = "address number one",
                    Photo = "files/photos/owner1.pdf",
                    Birthday = new DateTime(2000, 1, 10)
                },
                new Owner()
                {
                    IdOwner = 2,
                    Name = "Name owner second",
                    Address = "address number two",
                    Photo = "files/photos/owner2.pdf",
                    Birthday = new DateTime(2000, 2, 10)
                },
                new Owner()
                {
                    IdOwner = 3,
                    Name = "Name owner third",
                    Address = "address number three",
                    Photo = "files/photos/owner3.pdf",
                    Birthday = new DateTime(2000, 1, 10)
                });

            modelBuilder.Entity<Property>()
             .HasData(
               new Property()
               {
                   IdProperty = 1,
                   Name = "Property number one",
                   Address = "address property number one",
                   Price = 1500000,
                   CodeInternal = "0101",
                   Year = new DateTime(2005, 5, 10),
                   IdOwner = 1
               },
               new Property()
               {
                   IdProperty = 2,
                   Name = "Property number two",
                   Address = "address property number two",
                   Price = 1500000,
                   CodeInternal = "0101",
                   Year = new DateTime(2005, 5, 10),
                   IdOwner = 2
               },
               new Property()
               {
                   IdProperty = 3,
                   Name = "Property number three",
                   Address = "address property number three",
                   Price = 1500000,
                   CodeInternal = "0101",
                   Year = new DateTime(2005, 5, 10),
                   IdOwner = 3
               });

            base.OnModelCreating(modelBuilder);
        }

    }
}

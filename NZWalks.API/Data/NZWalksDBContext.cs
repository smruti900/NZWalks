using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext:DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed the data for Difficulties
            //Easy, Medium, Hard
            var difficulties=new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id=Guid.Parse("f1fe0c25-bc34-4538-aea7-46edb046e068"),
                    Name="Easy"
                },
                new Difficulty()
                {
                    Id=Guid.Parse("4a3538f1-22cf-40d0-8842-af3aa547ede6"),
                    Name="Medium"
                },
                new Difficulty()
                {
                    Id=Guid.Parse("2b5d06c1-308e-460d-9176-cde2618ec23a"),
                    Name="Hard"
                }
            };

            //Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            //Seed Data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id=Guid.Parse("2711575c-c119-4103-b66f-3408d076a887"),
                    Name="Auckland",
                    Code="AKL",
                    RegionImageUrl="https://a.travel-assets.com/findyours-php/viewfinder/images/res70/179000/179003-North-Island.jpg"
                },
                new Region()
                {
                    Id=Guid.Parse("249811b7-994b-4a86-b9c1-4a2257d3e18a"),
                    Name="SWitzerland",
                    Code="SWR",
                    RegionImageUrl="null"
                },
                new Region()
                {
                    Id=Guid.Parse("fdc7d5df-68a4-4871-8eef-d705bfd20125"),
                    Name="Bay of Plenty",
                    Code="BOF",
                    RegionImageUrl="https://www.journeysinternational.com/wp-content/uploads/2019/04/bay-of-plenty-aerial.jpg"
                },
                new Region()
                {
                    Id=Guid.Parse("923bf51b-4072-4e02-9d48-04cd0ee1d004"),
                    Name="Southland",
                    Code="STL",
                    RegionImageUrl="null    "
                },
                new Region()
                {
                    Id=Guid.Parse("4b83d1a0-c414-4b09-af4a-38ad84c2adef"),
                    Name="Nelson",
                    Code="NLS",
                    RegionImageUrl="https://mediaim.expedia.com/destination/1/f807c12da4e6ed31a1a61db1d9c1711a.jpg"
                },
                new Region()
                {
                    Id=Guid.Parse("bc3aac6e-fa85-4488-94e0-8f3066c896ff"),
                    Name="Wellington",
                    Code="WGL",
                    RegionImageUrl="https://a.travel-assets.com/findyours-php/viewfinder/images/res70/179000/179180-Wellington.jpg"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}

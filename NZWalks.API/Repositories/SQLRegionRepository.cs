using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext dbcontext;

        public SQLRegionRepository(NZWalksDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbcontext.Regions.AddAsync(region);
            await dbcontext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var existingregion=await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingregion == null)
            {
                return null;
            }
            dbcontext.Regions.Remove(existingregion);
            await dbcontext.SaveChangesAsync();
            return existingregion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbcontext.Regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingregion=await dbcontext.Regions.FirstOrDefaultAsync(y => y.Id == id);
            if (existingregion == null)
            {
                return null;
            }
            existingregion.Code=region.Code;
            existingregion.Name=region.Name;
            existingregion.RegionImageUrl=region.RegionImageUrl;

            await dbcontext.SaveChangesAsync();
            return existingregion;
        }
    }
} 

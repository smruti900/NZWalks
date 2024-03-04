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
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbcontext.Regions.ToListAsync();
        }
    }
}

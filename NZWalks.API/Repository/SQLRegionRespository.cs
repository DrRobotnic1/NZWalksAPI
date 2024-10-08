using NZWalks.API.Models.Domain;
using NZWalks.API.Data;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Repository
{
    public class SQLRegionRespository : IregionRespository
    {
        private readonly NZWalkDbContext _context;
        public SQLRegionRespository(NZWalkDbContext context)
        {
            _context = context;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _context.regions.FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Region?> CreateAsync(Region region)
        {
            await _context.regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _context.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null) { return null; }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _context.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingReg = await _context.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingReg == null)
            {
                return null;
            }
            _context.regions.Remove(existingReg);
            await _context.SaveChangesAsync();
            return existingReg;

        }




    }
}

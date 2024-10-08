using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repository
{
    public class SQLWalkRespository:IWalkRespository
    {
        private readonly NZWalkDbContext _context;

        public SQLWalkRespository(NZWalkDbContext context)
        {
            _context = context; 
        }
        public async Task<Walk?> CreateAsync(Walk walk)
        {
           await _context.walks.AddAsync(walk);
            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk?>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _context.walks.Include("Difficulty").Include("Region").AsQueryable();
            //return await _context.walks.Include("Difficulty").Include("Region").ToListAsync();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(x => x.Name) : walks.OrderBy(x => x.Name);
                }else if (sortBy.Equals("Lenght",StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(x => x.lengthInKm ) : walks.OrderByDescending(x => x.lengthInKm );
                }
            }

            //Pagination
            var skipResult = (pageNumber-1)*pageSize;

            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _context.walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id,Walk walk)
        {
             var existingDM =  await _context.walks.FirstOrDefaultAsync(y => y.Id == id);
            
            if (existingDM != null) { return null; }

            existingDM.Name = walk.Name;
            existingDM.Description = walk.Description;
           
            existingDM.lengthInKm = walk.lengthInKm;
            existingDM.walkImageUrl = walk.walkImageUrl;
            existingDM.RegionId = walk.RegionId;
            existingDM.DifficultyId = walk.DifficultyId;

            await _context.SaveChangesAsync();

            return existingDM;

        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk  = await _context.walks.FirstOrDefaultAsync(x =>x.Id == id);
            if (existingWalk != null)
            {
                return null;
            }
             _context.walks.Remove(existingWalk);
            _context.SaveChanges();
            return existingWalk;
        }
    }
}

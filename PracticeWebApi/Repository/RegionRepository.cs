using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeWebApi.Data;
using PracticeWebApi.Model.Domain;
using PracticeWebApi.Model.DTO;

namespace PracticeWebApi.Repository
{
	public class RegionRepository : IRegionRepository
	{
		private readonly WalksDbContext walksDbContext;
		private readonly IMapper mapper;

		public RegionRepository(WalksDbContext walksDbContext,IMapper mapper)
		{
			this.walksDbContext = walksDbContext;
			this.mapper = mapper;
		}

		public async Task<Region> CreateAsync(Region region)
		{
			
			await walksDbContext.Regions.AddAsync(region);
			await walksDbContext.SaveChangesAsync();
			return region;
		}

		public async Task<Region> DeleteAsync(Guid id)
		{
			var region = await walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
			
			walksDbContext.Regions.Remove(region);

			await walksDbContext.SaveChangesAsync();

			return region;
		}

		public async Task<List<Region>> GetAllAsync()
		{
			var region = await walksDbContext.Regions.ToListAsync();
			return region ;
		}

		public async Task<Region?> GetByIdAsync(Guid id)
		{
			var region = await walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			return region;
		}

		public async Task<Region> UpdateAsync(Guid id, Region region)
		{
			var regions = walksDbContext.Regions.FirstOrDefault(x => x.Id == id);

			regions.Name = region.Name;
			regions.RegionImageUrl =region.RegionImageUrl;
			regions.Code = region.Code;
			await walksDbContext.SaveChangesAsync();

			return regions;
		}

	}
}

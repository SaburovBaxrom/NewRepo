using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeWebApi.CustomValidation;
using PracticeWebApi.Model.Domain;
using PracticeWebApi.Model.DTO;
using PracticeWebApi.Repository;

namespace PracticeWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{
		private readonly IWalkRepository walkRepository;
		private readonly IMapper mapper;

		public WalksController(IWalkRepository walkRepository, IMapper mapper)
		{
			this.walkRepository = walkRepository;
			this.mapper = mapper;
		}

		[HttpPost]
		[ValidateModel]
		[Authorize(Roles = "Reader")]

		public async Task<IActionResult> CreateAsync([FromBody] AddWalkDto addWalkDto)
		{
			
				var walk = mapper.Map<Walk>(addWalkDto);

				await walkRepository.CreateAsync(walk);

				var walkDto = mapper.Map<WalkDto>(walk);

				return Ok(walkDto);
			
			
		}
		[HttpGet]
		[Authorize(Roles = "Reader")]

		public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
			[FromQuery] string? sortBy, [FromQuery] bool? isAscending,
			[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
		{
			var walks = await walkRepository.GetAllAsync(filterOn,filterQuery, sortBy, isAscending ?? true, pageNumber,pageSize);
			if(walks == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<List<WalkDto>>(walks));
		}
		[HttpGet]
		[Route("{id:Guid}")]
		[Authorize(Roles = "Reader")]

		public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
		{
			var walk = await walkRepository.GetByIdAsync(id);
			if (walk == null)
			{
				return NotFound();
			}

			var walkDto = mapper.Map<WalkDto>(walk);

			return Ok(walkDto);

		}

		[HttpDelete]
		[Route("{id:Guid}")]
		[Authorize(Roles = "Writer")]

		public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
		{
			var walk = await walkRepository.DeleteAsync(id);

			if(walk == null)
			{
				return NotFound();
			}
			
			var walkDto = mapper.Map<WalkDto>(walk);

			return Ok(walkDto);
		}


		[HttpPut]
		[Route("{id:Guid}")]
		[ValidateModel]
		[Authorize(Roles = "Writer")]

		public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
		{

			
				var walk = mapper.Map<Walk>(updateWalkDto);

				var responseWalk = await walkRepository.UpdateWalkAsync(id, walk);

				if (responseWalk == null)
				{
					return NotFound();
				}

				return Ok(mapper.Map<WalkDto>(responseWalk));
			
			
		}

	}
}

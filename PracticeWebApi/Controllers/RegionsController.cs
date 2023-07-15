using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeWebApi.CustomValidation;
using PracticeWebApi.Data;
using PracticeWebApi.Model.Domain;
using PracticeWebApi.Model.DTO;
using PracticeWebApi.Repository;

namespace PracticeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper _mapper;

        public RegionsController( IMapper mapper, IRegionRepository regionRepository)
        {
            this._mapper = mapper;
            this.regionRepository = regionRepository;
        }

        // GEt all regions
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var regions = await regionRepository.GetAllAsync();
            var regionsDto = new List<RegionDTO>();
            #region Mapping custom
            /*foreach(var region in regions)
            {
                regionsDto.Add(new RegionDTO
                {
                    Id= region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }*/
            #endregion
            return Ok(/*regions.Select(region => _mapper.Map<RegionDTO>(region))*/regions);
        
        }

        //Get single region (by regionId)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDTO>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionDto addRegionDto)
        {
            
				var region = _mapper.Map<Region>(addRegionDto);

				await regionRepository.CreateAsync(region);

				var regionDto = _mapper.Map<RegionDTO>(region);

				return CreatedAtAction(nameof(GetById), new { id = region.Id }, regionDto);
			
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            
				var region = _mapper.Map<Region>(updateRegionDto);
				var updatedRegion = await regionRepository.UpdateAsync(id, region);

				if (region == null)
				{
					return NotFound();
				}

				var regionDto = _mapper.Map<RegionDTO>(region);

				return Ok(regionDto);

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);
            if(region == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDTO>(region);
            return Ok(regionDto);
        }
    }
}

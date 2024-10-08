using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContext _context;
        private readonly IregionRespository _iregionRespository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalkDbContext context, IregionRespository iregionRespository,IMapper mapper)
        {
            _context = context;
            _iregionRespository = iregionRespository;
            _mapper = mapper;


        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            //Get Data From Database - Domain models
            var regionDomainModel = await _iregionRespository.GetAllAsync();
           var regionDto = _mapper.Map<List<RegionDto>>(regionDomainModel);

            return Ok(regionDto);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            var region = await _iregionRespository.GetByIdAsync(id);

            if (region == null) { return NotFound(); }
            return Ok(_mapper.Map <RegionDto> (region));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
           
                var regionDM = _mapper.Map<Region>(addRegionRequestDto);
                await _iregionRespository.CreateAsync(regionDM);
                var regionDto = _mapper.Map<RegionDto>(regionDM);

                return CreatedAtAction(nameof(GetById), new { id = regionDM.Id }, regionDto);
           
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
           
                //map DTO to Model
                var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

                regionDomainModel = await _iregionRespository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null) { return NotFound(); }

                var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

                await _context.SaveChangesAsync();

                return Ok(regionDto);
            
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteById([FromRoute]Guid id) 
        {
            var regionDM = await _iregionRespository.DeleteAsync(id);

             if(regionDM == null) { return NotFound(); }

            var regionDto = _mapper.Map<RegionDto>(regionDM);
            return Ok(regionDto); 
        }

    }
}

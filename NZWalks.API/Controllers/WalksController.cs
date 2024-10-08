using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRespository _walk;
        public WalksController(IMapper mapper,IWalkRespository walk)
        {
            _mapper = mapper;
            _walk = walk;
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] AddWalsRequestDto addWalsRequestDto)
        {
            
                var walkDM = _mapper.Map<Walk>(addWalsRequestDto);
                if (walkDM == null) { return NotFound(); }

                await _walk.CreateAsync(walkDM);

                var walkDTO = _mapper.Map<WalkDto>(walkDM);

                return Ok(walkDTO);
           
        }

        [HttpGet]
        ///api/walks?filetrOn=Name&filterQuery=Track
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery]string sortBy,[FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery]int pageSize = 1000)
        {
            var walkDM = await _walk.GetAllAsync(filterOn,filterQuery,sortBy,isAscending,pageNumber,pageSize);

            var walkDTO = _mapper.Map<List<WalkDto>>(walkDM);

            return Ok(walkDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var walkDM = await _walk.GetByIdAsync(id);

            var walkDTO = _mapper.Map<WalkDto>(walkDM);

            return Ok(walkDTO);

        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id,UpdateWalkRequest updateWalkRequest)
        {
                var walkDM = _mapper.Map<Walk>(updateWalkRequest);

                await _walk.UpdateAsync(id, walkDM);

                var WalkDTO = _mapper.Map<WalkDto>(walkDM);
            return Ok(WalkDTO);
           
           
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            {
                var deleteWalk = await _walk.DeleteAsync(id);

                if (deleteWalk != null)
                {
                    return NotFound();
                }

                var walkDTO = _mapper.Map<WalkDto>(deleteWalk);
                return Ok(walkDTO);

            }
        }

        
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //if (ModelState.IsValid)
            //{
                //Map add walk request DTO to walk Domain model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                //Map Domain model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}
        }

        //Get Walks
        //GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&ispageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=1000)
        {
            var walksDomainModel = await walkRepository.GetWalksAsync(filterOn, filterQuery, sortBy, isAscending ?? true, 
                pageNumber, pageSize);

            //create an exception
            throw new Exception("This is a new exception");

            //Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        //Get Walks By Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkdomainModel = await walkRepository.GetWalkByIdAsync(id);
            if (walkdomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkdomainModel)); 
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModelAttribute]
        public async Task<IActionResult> UpdateWalkById([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //if (ModelState.IsValid)
            //{
                //Map Dto TO Domain Model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
                walkDomainModel = await walkRepository.UpdateWalkByIdAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                //Map Domain Model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWalkById(Guid id)
        {
            var walkDomainModel =await walkRepository.DeleteWalkByIdAsync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}

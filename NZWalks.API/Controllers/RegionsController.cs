using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Threading.Tasks;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;

        public IRegionRepository RegionRepository { get; }

        public RegionsController(NZWalksDBContext dbContext,IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            RegionRepository = regionRepository;
        }
        //Get All regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from Database - Domain Models
            var regionsDomain = await RegionRepository.GetAllAsync();

            //map Domain models to DTOs
            var regionsdto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsdto.Add(new RegionDto()
                {
                    Id= regionDomain.Id,
                    Name= regionDomain.Name,
                    Code= regionDomain.Code,
                    RegionImageUrl= regionDomain.RegionImageUrl
                });
            }

            //Return DTOs
            return Ok(regionsdto);
        }
        //Get Single region (get region by Id)
        [HttpGet]
        [Route("{id:Guid}", Name = "GetById")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            //Get Region Domain Model from Database
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Region Dto
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            //Return DTO back to Client
            return Ok(regionDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDmomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //use Domain Model to create Region
            await dbContext.Regions.AddAsync(regionDmomainModel);
            await dbContext.SaveChangesAsync();

            //Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDmomainModel.Id,
                Code = regionDmomainModel.Code,
                Name = regionDmomainModel.Name,
                RegionImageUrl = regionDmomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }
        //Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regiondomainmodel= await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(regiondomainmodel == null)
            {
                return NotFound();
            }
            //Map DTO to domain model
            regiondomainmodel.Code= updateRegionRequestDto.Code;
            regiondomainmodel.Name= updateRegionRequestDto.Name;
            regiondomainmodel.RegionImageUrl= updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            var regionDto = new RegionDto
            {
                Id = regiondomainmodel.Id,
                Code = regiondomainmodel.Code,
                Name = regiondomainmodel.Name,
                RegionImageUrl = regiondomainmodel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if( regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete region
            dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();

            //return deleted region back
            //map domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}

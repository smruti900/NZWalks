using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;

        public RegionsController(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get All regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get data from Database - Domain Models
            var regionsDomain=dbContext.Regions.ToList();

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
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            //Get Region Domain Model from Database
            var regionDomain = dbContext.Regions.FirstOrDefault(x=>x.Id==id);
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
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDmomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //use Domain Model to create Region
            dbContext.Regions.Add(regionDmomainModel);
            dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regiondomainmodel=dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(regiondomainmodel == null)
            {
                return NotFound();
            }
            //Map DTO to domain model
            regiondomainmodel.Code= updateRegionRequestDto.Code;
            regiondomainmodel.Name= updateRegionRequestDto.Name;
            regiondomainmodel.RegionImageUrl= updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();
            var regionDto = new RegionDto
            {
                Id = regiondomainmodel.Id,
                Code = regiondomainmodel.Code,
                Name = regiondomainmodel.Name,
                RegionImageUrl = regiondomainmodel.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}

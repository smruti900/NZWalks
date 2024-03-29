﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;
using System.Threading.Tasks;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDBContext dbContext,IRegionRepository regionRepository,IMapper mapper,ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        //Get All regions
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                throw new Exception("This is a custom Exception");
                //logger.LogInformation("GetAllRegions Action Method Invoke");

                //logger.LogWarning("This is a warning log");

                //logger.LogError("This is a error log");

                //Get data from Database - Domain Models
                var regionsDomain = await regionRepository.GetAllAsync();

                //map Domain models to DTOs
                //var regionsdto = new List<RegionDto>();
                //foreach (var regionDomain in regionsDomain)
                //{
                //    regionsdto.Add(new RegionDto()
                //    {
                //        Id= regionDomain.Id,
                //        Name= regionDomain.Name,
                //        Code= regionDomain.Code,
                //        RegionImageUrl= regionDomain.RegionImageUrl
                //    });
                //}
                logger.LogInformation($"Finished GetAllRegions request with Data: {JsonSerializer.Serialize(regionsDomain)}");

                var regionsdto = mapper.Map<List<RegionDto>>(regionsDomain);

                //Return DTOs
                return Ok(regionsdto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                throw;
            }
            
        }

        //Get Single region (get region by Id)
        [HttpGet]
        [Authorize(Roles = "Reader")]
        [Route("{id:Guid}", Name = "GetById")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            //Get Region Domain Model from Database
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Region Dto
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};
            //Return DTO back to Client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //POST to Create a New Region
        [HttpPost]
        [ValidateModelAttribute]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //if (ModelState.IsValid)
            //{

                //Map or Convert DTO to Domain Model
                var regionDmomainModel = mapper.Map<Region>(addRegionRequestDto);
                //    new Region
                //{
                //    Code = addRegionRequestDto.Code,
                //    Name = addRegionRequestDto.Name,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
                //};

                //use Domain Model to create Region
                var regionDomainModel = await regionRepository.CreateAsync(regionDmomainModel);

                //Map Domain model back to DTO
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                //    new RegionDto
                //{
                //    Id = regionDmomainModel.Id,
                //    Code = regionDmomainModel.Code,
                //    Name = regionDmomainModel.Name,
                //    RegionImageUrl = regionDmomainModel.RegionImageUrl
                //};

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}
        }

        //Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModelAttribute]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //if (ModelState.IsValid)
            //{
                //Map DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
                //    new Region
                //{
                //    Code = updateRegionRequestDto.Code,
                //    Name = updateRegionRequestDto.Name,
                //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
                //};

                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                //    = new RegionDto
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageUrl = regionDomainModel.RegionImageUrl
                //};

                return Ok(regionDto);
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}
        }

        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if( regionDomainModel == null)
            {
                return NotFound();
            }

            //return deleted region back
            //map domain model to DTO
            var regionDto = mapper.Map<RegionDto> (regionDomainModel);
            //    = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            return Ok(regionDto);
        }
    }
}

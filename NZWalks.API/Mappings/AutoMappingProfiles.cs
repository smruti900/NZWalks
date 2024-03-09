﻿using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMappingProfiles:Profile
    {
        public AutoMappingProfiles()
        {
            //CreateMap<UserDTO, UserDomain>()
            //    .ForMember(x=>x.Name,opt=>opt.MapFrom(x=>x.FullName))
            //    .ReverseMap();
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }
    }

    //public class UserDTO
    //{
    //    public string FullName { get; set; }
    //}
    //public class UserDomain
    //{
    //    public string Name { get; set; }
    //}
}

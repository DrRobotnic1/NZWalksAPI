﻿using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings

{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
          CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,RegionDto>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();
            CreateMap<AddWalsRequestDto,Walk>().ReverseMap();
            CreateMap<Walk , WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkRequest, Walk>().ReverseMap();
            CreateMap<Region,AddRegionRequestDto>().ReverseMap();
          
            
        }
    }
}

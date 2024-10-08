﻿using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repository
{
    public interface IWalkRespository
    {
        Task<Walk?> CreateAsync(Walk walk);
        Task<List<Walk?>> GetAllAsync(string? filterOn = null,string? filterQuery = null,string? sortBy = null, bool? isAscending = true, int pageNumber = 1,int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id,Walk walk);
        Task<Walk?> DeleteAsync(Guid id);


    }
}

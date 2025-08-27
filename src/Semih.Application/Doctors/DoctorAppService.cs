using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Semih.Doctors;

public class DoctorAppService : 
    CrudAppService<Doctor, DoctorDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateDoctorDto>,
    IDoctorAppService
{
    private readonly ILogger<DoctorAppService> _logger;

    public DoctorAppService(IRepository<Doctor, Guid> repository, ILogger<DoctorAppService> logger) 
        : base(repository)
    {
        _logger = logger;
    }

    public override async Task<DoctorDto> CreateAsync(CreateUpdateDoctorDto input)
    {
        try
        {
            var entity = await MapToEntityAsync(input);
            await Repository.InsertAsync(entity, true);
            return await MapToGetOutputDtoAsync(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create doctor. Input: {@Input}", input);
            throw;
        }
    }

    public override async Task<DoctorDto> UpdateAsync(Guid id, CreateUpdateDoctorDto input)
    {
        try
        {
            var entity = await Repository.GetAsync(id);
            await MapToEntityAsync(input, entity);
            await Repository.UpdateAsync(entity, true);
            return await MapToGetOutputDtoAsync(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update doctor. Id: {Id}, Input: {@Input}", id, input);
            throw;
        }
    }
}

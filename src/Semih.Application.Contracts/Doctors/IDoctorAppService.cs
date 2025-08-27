using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

namespace Semih.Doctors
{
    public interface IDoctorAppService : ICrudAppService<
        DoctorDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateDoctorDto>
    {
    }
}
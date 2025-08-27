using System;
using Volo.Abp.Domain.Repositories;

namespace Semih.Doctors;

public interface IDoctorRepository : IRepository<Doctor, Guid>
{
    
}
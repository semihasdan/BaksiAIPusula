using System;
using Volo.Abp.Application.Dtos;

namespace Semih.Doctors;

    public class DoctorDto : AuditedEntityDto<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
    }
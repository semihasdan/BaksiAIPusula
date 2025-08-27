using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Semih.Doctors
{
    public class Doctor : FullAuditedAggregateRoot<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;

        protected Doctor()
        {
        }

        public Doctor(
            Guid id,
            string firstName,
            string lastName,
            string specialty) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Specialty = specialty;
        }
    }
}
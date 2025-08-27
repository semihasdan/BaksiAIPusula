using AutoMapper;
using Semih.Doctors;

namespace Semih
{
    public class DoctorApplicationAutoMapperProfile : Profile
    {
        public DoctorApplicationAutoMapperProfile()
        {
            CreateMap<Doctor, DoctorDto>();
            CreateMap<CreateUpdateDoctorDto, Doctor>();
        }
    }
}

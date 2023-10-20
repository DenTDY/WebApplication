using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress,
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            
            CreateMap<House, HouseDto>()
                .ForMember(c => c.AddressAndNumberFloors,
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.NumberFloors)));

            CreateMap<Employee, EmployeeDto>();
            CreateMap<Apartment, ApartmentDto>();
        }
    }
}

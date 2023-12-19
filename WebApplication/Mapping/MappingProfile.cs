using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApplication.Mapping
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

            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<HouseForCreationDto, House>();
            CreateMap<ApartmentForCreationDto, Apartment>();

            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<EmployeeForUpdateDto, Employee>();
            CreateMap<HouseForUpdateDto, House>();
            CreateMap<ApartmentForUpdateDto, Apartment>();
        }
    }
}

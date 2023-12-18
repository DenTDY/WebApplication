using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System.ComponentModel.Design;

namespace Repository
{
    public class ApartmentRepository : RepositoryBase<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateApartmentForHouse(Guid houseId, Apartment apartment)
        {
            apartment.HouseId= houseId;
            Create(apartment);
        }

        public void DeleteApartment(Apartment apartment)
        {
            Delete(apartment);
        }

        public async Task<Apartment> GetApartmentAsync(Guid houseId, Guid id, bool trackChanges)
        {
            return await FindByCondition(
                e => e.HouseId.Equals(houseId) && e.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<PagedList<Apartment>> GetApartmentsAsync(Guid houseId, ApartmentParameters apartmentParameters, bool trackChanges)
        {
            var apartments = await FindByCondition(e => e.HouseId.Equals(houseId), trackChanges)
                .FilterApartments(apartmentParameters.MinNumberRoom, apartmentParameters.MaxNumberRoom)
                .Search(apartmentParameters.SearchTerm)
                .Sort(apartmentParameters.OrderBy)
                .ToListAsync();

            return PagedList<Apartment>.ToPagedList(apartments, apartmentParameters.PageNumber, apartmentParameters.PageSize);
        }

        public void TestApartment()
        {
           
        }
    }
}

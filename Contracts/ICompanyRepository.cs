using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        public void TestCompany();
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
    }
}

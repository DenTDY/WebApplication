using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {

        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private IHouseRepository _houseRepository;
        private IApartmentRepository _apartmentRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }

        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }

        public IHouseRepository House
        {
            get
            {
                if (_houseRepository == null)
                    _houseRepository = new HouseRepository(_repositoryContext);
                return _houseRepository;
            }
        }

        public IApartmentRepository Apartment
        {
            get
            {
                if (_apartmentRepository == null)
                    _apartmentRepository = new ApartmentRepository(_repositoryContext);
                return _apartmentRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}

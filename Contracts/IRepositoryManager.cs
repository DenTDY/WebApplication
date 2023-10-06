namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IHouseRepository House { get; }
        IApartmentRepository Apartment { get; }
        void Save();
    }
}

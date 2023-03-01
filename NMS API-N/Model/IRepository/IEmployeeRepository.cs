namespace NMS_API_N.Model.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<object>> GetEmployeeDropdown();
    }
}

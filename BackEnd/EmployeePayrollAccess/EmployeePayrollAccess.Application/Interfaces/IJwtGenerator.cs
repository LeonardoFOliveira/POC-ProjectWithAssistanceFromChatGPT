namespace EmployeePayrollAccess.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string Generate(int userId);
    }
}

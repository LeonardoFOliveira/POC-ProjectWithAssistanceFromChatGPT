namespace EmployeePayrollAccess.Application.Common
{
    public class ServiceResult<T>
    {
        public T Data { get; }
        public bool Success { get; }
        public string ErrorMessage { get; }

        public ServiceResult(bool success, T data = default, string errorMessage = null)
        {
            Success = success;
            if (success)
            {
                Data = data;
            }
            else
            {
                ErrorMessage = errorMessage;
            }
        }
    }
}

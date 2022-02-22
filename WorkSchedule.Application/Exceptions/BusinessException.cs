
namespace WorkSchedule.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public int ErrorCode { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}

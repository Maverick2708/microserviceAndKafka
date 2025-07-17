namespace SalesManagement.Application.Common.Models
{
    public class ApiResponseModel<T>
    {
        public int Status { get; set; } // ??i sang ki?u s?
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}

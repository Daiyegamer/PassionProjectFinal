namespace AdilBooks.Models
{
    public class ServiceResponse
    {
        public enum ServiceStatus
        {
            Success,
            Error,
            Created,
            Updated,
            Deleted,
            NotFound
        }

        public ServiceStatus Status { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public int CreatedId { get; set; } // For when a new record is created
        public object Data { get; set; } // This will hold the data returned by the service (optional)
    }
}

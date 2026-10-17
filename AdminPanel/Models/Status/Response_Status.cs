
namespace AdminPanel.Models.Status
{
    public class Response_Status
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public List<StatusVM> status { get; set; } = new List<StatusVM>();
    }
}

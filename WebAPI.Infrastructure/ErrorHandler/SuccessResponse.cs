namespace WebAPI
{
    public class SuccessResponse
    {
        public string Status { get; set; }
        public dynamic? Data { get; set; }
        public SuccessResponse()    
        {
            Status = "";
            Data = "";
        }
        public SuccessResponse(string status, dynamic data) { Status = status; Data = data; }
        public SuccessResponse(string status) { Status = status;  }
    }
}

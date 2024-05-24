namespace WebAPI
{
    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }
        public string UserMessage { get; set; }
        public ErrorResponse(string errorMessage, string userMessage) 
        { 
            this.ErrorMessage = errorMessage;   
            this.UserMessage = userMessage;  
        }

        public ErrorResponse(string userMessage)
        {
            this.ErrorMessage = "";
            this.UserMessage = userMessage;
        }
    }
}

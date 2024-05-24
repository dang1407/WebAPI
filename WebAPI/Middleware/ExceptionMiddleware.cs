using WebAPI.Domain;

namespace WebAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; 

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;   
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex) 
            {
                await HandleExceptionAsync(context, ex);
            }

        }


        /// <summary>
        /// Hàm xử lý mã lỗi chung
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="exception">Ngoại lệ xảy ra</param>
        /// <returns>Exception theo dạng ErrorCode, UserMessage, DevMessage, TraceId, MoreInfo</returns>
        /// Create by: nkmdang (18/09/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine(exception);  
            context.Response.ContentType = "application/json";

            if(exception is NotFoundException notFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = notFoundException.ErrorCode,
                        UserMessage = notFoundException.UserMessage,
#if DEBUG
                        DevMessage = notFoundException.Message,
#else
                        DevMessage = "",
#endif
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                    }.ToString() ?? ""
                    );
            } else if(exception is ConflictException conflictException)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = conflictException.ErrorCode,
                        UserMessage = conflictException.UserMessage,
#if DEBUG
                        DevMessage = conflictException.Message,
#else
                        DevMessage = "",
#endif
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                    }.ToString() ?? ""
                    );
            } else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = context.Response.StatusCode,
                        UserMessage = "Lỗi hệ thống",
#if DEBUG
                        DevMessage = exception.Message,
#else
                        DevMessage = "",
#endif
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                    }.ToString() ?? ""
                    );
            }
        }
    }
}

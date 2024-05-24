using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class NotFoundException : Exception
    {
        public int ErrorCode { get; set; }
        public string DevMessage { get; set; }

        public string UserMessage { get; set; }
        public NotFoundException() { }

        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Phương thức khởi tạo NotFoundException cho UserMessage
        /// </summary>
        /// <param name="message">Nội dung NotFoundException</param>
        /// Created by: nkmdang (18/09/2023)
        public NotFoundException(string userMessage) 
        {
            UserMessage = userMessage;
        }


        /// <summary>
        /// Phương thức khởi tạo NotFoundException cho UserMessage và ErrorCode
        /// </summary>
        /// <param name="message">Nội dung NotFoundException</param>
        /// <param name="errorCode">Mã lỗi của NotFoundException</param>
        /// Created by: nkmdang (18/09/2023)
        public NotFoundException(string userMessage, int errorCode) 
        {
            UserMessage = userMessage;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Phương thức khởi tạo với 3 tham số
        /// </summary>
        /// <param name="devMessage">Thông báo cho lập trình viên</param>
        /// <param name="userMessage">Thông báo cho người dùng</param>
        /// <param name="errorCode">Mã lỗi cho người dùng</param>
        /// Created by: nkmdang (18/09/2023)
        public NotFoundException(string devMessage , string userMessage, int errorCode)
        {
            DevMessage = devMessage;
            UserMessage = userMessage;  
            ErrorCode = errorCode;
        }
    }
}

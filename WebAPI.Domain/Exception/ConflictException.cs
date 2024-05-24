using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class ConflictException : Exception
    {
        public int ErrorCode { get; set; }

        public string DevMessage { get; set; }

        public string UserMessage { get; set; }

        public ConflictException() { }

        public ConflictException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Phương thức khởi tạo ConflictException với UserMessage
        /// </summary>
        /// <param name="message">Nội dung ConflictException</param>
        /// Created by: nkmdang (18/09/2023)
        public ConflictException(string userMessage)
        {
            UserMessage = userMessage;
        }


        /// <summary>
        /// Phương thức khởi tạo ConflictException với UserMessage và ErrorCode
        /// </summary>
        /// <param name="message">Nội dung ConflictException</param>
        /// <param name="errorCode">Mã lỗi của ConflictException</param>
        /// Created by: nkmdang (18/09/2023)
        public ConflictException(string userMessage, int errorCode) 
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
        public ConflictException(string devMessage, string userMessage, int errorCode)
        {
            DevMessage = devMessage;
            UserMessage = userMessage;
            ErrorCode = errorCode;
        }
    }
}

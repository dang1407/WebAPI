using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Domain
{
    public class BaseException
    {
        #region Properties
        public int ErrorCode { get; set; }  
        public string? DevMessage { get; set; }
        public string? UserMessage { get; set; }    
        public string? TraceId { get; set; }
        public string? MoreInfo { get; set;}
        public object? Errors { get; set; } 
        
        public object? ErrorKeys { get; set; }
        #endregion

        /// <summary>
        /// Hàm ghi đè hàm ToString mặc định của Object
        /// </summary>
        /// <returns>Chuỗi chứa nội dung của class</returns>
        /// Created by: nkmdang (18/09/2023)
        #region Override method ToString()
        public override string ToString()
        {
            // Configure JsonSerializerOptions
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                // Set encoding to UTF-8
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,

                // Indent the JSON for readability
                WriteIndented = true
            };

            // Serialize the object to JSON using the configured options
            //var jsonString = JsonSerializer.Serialize(this, jsonSerializerOptions);

            // Return the JSON response
            //return Content(jsonString, "application/json; charset=utf-8");
            //return JsonSerializer.Serialize(this, jsonSerializerOptions);
            return JsonSerializer.Serialize(this, jsonSerializerOptions);
        }
        #endregion
    }
}

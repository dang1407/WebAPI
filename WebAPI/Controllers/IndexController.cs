
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Reflection;

using System.Resources;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private  ResourceManager _resourceManager;  

        public IndexController()
        {
            _resourceManager = new ResourceManager(typeof(Assembly));
        }

        [HttpGet]
        [Route("")]
        public ContentResult Index() 
        {
            
            return base.Content("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n    <title>Document</title>\r\n  </head>\r\n  <body>\r\n    <h1>Đây là API của Nguyễn Khánh Minh Đăng</h1>\r\n    <div style=\"display: flex\">\r\n      <a href=\"/Swagger/index.html\">API Swagger</a>\r\n    </div>\r\n  </body>\r\n</html>\r\n", "text/html");
        }

        [HttpPost]
        [Route("test-resources")]
        public  IActionResult TestResource([FromBody] IndexDTO index, string language)
        {
            string[] languageDictionary = ["vi-VN", "en-US"];
            if(languageDictionary.Contains(language) )
            { 
                //using (ResXResourceSet resxSet = new ResXResourceSet(resxFile))
                //{

                //}
                    return Ok(new { language = language, resource = _resourceManager.GetString("IndexAPIHello")  });
            } 
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("jwtData")]
        public IActionResult GetJwtData()
        {
            var user = HttpContext.User.FindFirstValue("CompanyId");
            return Ok(user);    
        }

        public class IndexDTO
        {
            public string Name { get; set; }    
        }
    }
}

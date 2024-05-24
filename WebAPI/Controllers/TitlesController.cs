using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Controllers
{
    public class TitlesController : BaseCompanyController<TitleDTO, TitleCreatedDTO, TitleUpdateDTO>
    {
        
        public TitlesController(ITitleService titleService) : base(titleService)
        {

        }
    }
}

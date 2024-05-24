using AutoMapper;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class TitleService : BaseCompanyService<Title, TitleDTO, TitleCreatedDTO, TitleUpdateDTO>, ITitleService
    {

        public TitleService(ITitleRepository titleRepository, IMapper mapper) : base(titleRepository, mapper)
        {
        }
    }
}

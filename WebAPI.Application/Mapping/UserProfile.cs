using AutoMapper;
using WebAPI.Domain;

namespace WebAPI.Application
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();
            CreateMap<AccountCreateDTO, Account>();
            CreateMap<AccountUpdateDTO, Account>();
        }    
    }
}

using API.RequestModels.Accounts;
using API.ResponseModels.Accounts;
using Data.Models;

namespace API.Helpers
{
    public class PetProfile : AutoMapper.Profile
    {
        public PetProfile()
        {
            CreateMap<Account, CreateAccount>().ReverseMap();
            CreateMap<Account, AccountResponse>().ReverseMap();
        }
    }
}

using AutoMapper;
using BaseInMemoryArchitecture.Models.Models;
using BaseInMemoryArchitecture.Web.ViewModels;

namespace BaseInMemoryArchitecture.Web.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserVM, User>()
                .ReverseMap();
            CreateMap<ClientVM, Client>()
                   .ReverseMap();
        }
    }
}

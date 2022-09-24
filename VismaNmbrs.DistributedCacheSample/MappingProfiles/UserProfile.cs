using AutoMapper;
using VismaNmbrs.DistributedCacheSample.Entities;
using VismaNmbrs.DistributedCacheSample.ViewModel;

namespace VismaNmbrs.DistributedCacheSample.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest =>
                    dest.BirthDate,
                    opt => opt.MapFrom(src => src.BirthDate.ToString("MM/dd/yyyy")));
        }
    }
}

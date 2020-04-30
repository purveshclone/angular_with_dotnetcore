using System.Linq;
using AutoMapper;
using Testfirst.API.Dtos;
using Testfirst.API.Models;

namespace Testfirst.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Users,UserForListDto>()
            .ForMember(dest=>dest.PhotoUrl,opt=>
                            opt.MapFrom(src=>
                                src.Photos.FirstOrDefault(p=>p.IsMain).Url))
            .ForMember(dest=>dest.Age,opt=>opt.MapFrom(src=>
                            src.DateOfBirth.CalculateAge()));
            CreateMap<Users,UserForDetailDto>()
             .ForMember(dest=>dest.PhotoUrl,opt=>
                            opt.MapFrom(src=>
                                src.Photos.FirstOrDefault(p=>p.IsMain).Url))
            .ForMember(dest=>dest.Age,opt=>opt.MapFrom(src=>
                            src.DateOfBirth.CalculateAge()));;
            CreateMap<Photo,PhotoForDetailDto>();
            CreateMap<UserForUpdateDto, Users>();
        }
    }
}
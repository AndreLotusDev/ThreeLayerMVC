using AutoMapper;
using ThreeLayer.API.Models.DTOS;
using ThreeLayer.Business.Models;

namespace ThreeLayer.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<BrazilianPersonCreateDTO, BrazilianPerson>();
            CreateMap<BrazilianPersonDTO, BrazilianPerson>().ReverseMap();
        }
    }
}

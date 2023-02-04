using AutoMapper;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;

namespace MagicVilla_API
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();
            CreateMap<VillaCreateDto, Villa>().ReverseMap();
            CreateMap<VillaUpdateDto, Villa>().ReverseMap();
        }
    }
}

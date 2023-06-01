using AutoMapper;
using TreeStructureAPI.Models;
using TreeStructureAPI.Models.Dto;

namespace TreeStructureAPI.Mappers;

public class ItemMappingProfile : Profile
{
    public ItemMappingProfile()
    {
        CreateMap<Item, GetItemDto>()
            .ForMember(dest => dest.ChildItems, opt => opt.MapFrom(src => src.ChildItems));
        CreateMap<GetItemDto, Item>();
        CreateMap<IOrderedEnumerable<Item>, GetItemDto>();
    }
}
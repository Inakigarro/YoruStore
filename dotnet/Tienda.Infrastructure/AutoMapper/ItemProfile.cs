using AutoMapper;
using Tienda.Contracts.Items;
using Tienda.Domain;

namespace Tienda.Infrastructure.AutoMapper;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        this.CreateMap<Item, ItemDto>()
            .ForMember(dto => dto.Id, s => s.MapFrom(entity => entity.Id))
            .ForMember(dto => dto.Titulo, s => s.MapFrom(entity => entity.Titulo))
            .ForMember(dto => dto.Descripcion, s => s.MapFrom(entity => entity.Descripcion))
            .ForMember(dto => dto.Precio, s => s.MapFrom(entity => entity.Precio))
            .ForMember(dto => dto.CategoriaId, s => s.MapFrom(entity => entity.CategoriaId));

        this.CreateMap<ItemDto, Item>()
            .ForMember(entity => entity.Id, s => s.MapFrom(entity => entity.Id))
            .ForMember(entity => entity.Titulo, s => s.MapFrom(entity => entity.Titulo))
            .ForMember(entity => entity.Descripcion, s => s.MapFrom(entity => entity.Descripcion))
            .ForMember(entity => entity.Precio, s => s.MapFrom(entity => entity.Precio));
    }
}

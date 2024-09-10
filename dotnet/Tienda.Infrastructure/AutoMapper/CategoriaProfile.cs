using AutoMapper;
using Tienda.Contracts.Categorias;
using Tienda.Domain;

namespace Tienda.Infrastructure.AutoMapper;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        this.CreateMap<Categoria, CategoriaDto>()
            .ForMember(dto => dto.Id, s => s.MapFrom(entity => entity.Id))
            .ForMember(dto => dto.Nombre, s => s.MapFrom(entity => entity.Nombre));
    }
}

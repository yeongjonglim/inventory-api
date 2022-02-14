using AutoMapper;
using InventoryAPI.Application.Common.Mappings;
using InventoryAPI.Domain.Entities;

namespace InventoryAPI.Application.Tyres.Queries;

public class TyrePriceBriefDto : IMapFrom<TyrePrice>
{
    public int Width { get; set; }
    public int Profile { get; set; }
    public int Diameter { get; set; }
    public decimal SellingPrice { get; set; }
    public string? Brand { get; set; }
    public string? Series { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TyrePrice, TyrePriceBriefDto>()
            .IncludeMembers(x => x.TyrePattern, x => x.TyreSize);

        profile.CreateMap<TyrePattern, TyrePriceBriefDto>();
        
        profile.CreateMap<TyreSize, TyrePriceBriefDto>();
    }
}
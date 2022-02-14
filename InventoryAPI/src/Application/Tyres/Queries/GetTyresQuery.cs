using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Application.Common.Interfaces;

namespace InventoryAPI.Application.Tyres.Queries;

public class GetTyresQuery : IRequest<TyrePriceResponse>
{
}

public class GetTyresQueryHandler : IRequestHandler<GetTyresQuery, TyrePriceResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTyresQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TyrePriceResponse> Handle(GetTyresQuery request, CancellationToken cancellationToken)
    {
        return new TyrePriceResponse
        {
            Tyres = await _context.TyrePrices
                .AsNoTracking()
                .ProjectTo<TyrePriceBriefDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Diameter).ThenBy(t => t.Width)
                .ToListAsync(cancellationToken)
        };
    }
}
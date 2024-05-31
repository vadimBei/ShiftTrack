﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShiftTrack.Authentication.Interfaces;
using ShiftTrack.Core.Application.Data.Common.Interfaces;
using ShiftTrack.Core.Application.Organization.Structure.Common.ViewModels;

namespace ShiftTrack.Core.Application.Organization.Structure.Positions.Queries.GetPositions
{
    public class GetPositionsQueryHandler : IRequestHandler<GetPositionsQuery, IEnumerable<PositionVM>>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _applicationDbContext;

        public GetPositionsQueryHandler(
            IMapper mapper,
            ICurrentUserService currentUserService,
            IApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<PositionVM>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            var carrentUser = _currentUserService.User;

            var positions = await _applicationDbContext.Positions
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<PositionVM>>(positions);
        }
    }
}

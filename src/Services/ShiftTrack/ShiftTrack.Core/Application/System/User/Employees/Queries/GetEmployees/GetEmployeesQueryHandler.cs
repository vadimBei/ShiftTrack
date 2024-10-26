﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShiftTrack.Core.Application.Data.Common.Interfaces;
using ShiftTrack.Core.Application.Organization.Structure.Common.Interfaces;
using ShiftTrack.Core.Application.System.User.Common.ViewModels;

namespace ShiftTrack.Core.Application.System.User.Employees.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeVM>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitService _unitService;
        private readonly IDepartmentService _departmentService;
        private readonly IApplicationDbContext _applicationDbContext;

        public GetEmployeesQueryHandler(
            IMapper mapper, 
            IUnitService unitService, 
            IDepartmentService departmentService, 
            IApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _unitService = unitService;
            _departmentService = departmentService;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<EmployeeVM>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employeeQuery = _applicationDbContext.Employees
                .Include(x => x.Department)
                    .ThenInclude(x => x.Unit)
                .Include(x => x.Position)
                .AsQueryable();

            if (request.UnitId is not null)
            {
                await _unitService.GetById(request.UnitId, cancellationToken);

                employeeQuery = employeeQuery
                    .Where(x => x.Department.UnitId == request.UnitId);
            }

            if (request.DepartmentId is not null)
            {
                await _departmentService.GetById(request.DepartmentId, cancellationToken);

                employeeQuery = employeeQuery
                    .Where(x => x.DepartmentId == request.DepartmentId);
            }

            if (!string.IsNullOrWhiteSpace(request.SearchPattern))
            {
                employeeQuery = employeeQuery
                    .Where(x => EF.Functions.Like(
                        x.Surname.ToLower() + " " + x.Name.ToLower() + " " + x.Patronymic.ToLower(),
                        $"%{request.SearchPattern.ToLower()}%"));
            }

            var employees = await employeeQuery
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<EmployeeVM>>(employees);
        }
    }
}

﻿using MediatR;
using ShiftTrack.Core.Application.System.User.Common.ViewModels;

namespace ShiftTrack.Core.Application.System.User.Employees.Queries.GetEmployees
{
    public record GetEmployeesQuery(
        string SearchPattern) : IRequest<IEnumerable<EmployeeVM>>;
}
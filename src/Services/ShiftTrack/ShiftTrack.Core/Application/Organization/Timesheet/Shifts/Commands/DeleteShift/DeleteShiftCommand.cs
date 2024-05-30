﻿using MediatR;

namespace ShiftTrack.Core.Application.Organization.Timesheet.Shifts.Commands.DeleteShift
{
    public record DeleteShiftCommand(
        long Id) : IRequest;
}

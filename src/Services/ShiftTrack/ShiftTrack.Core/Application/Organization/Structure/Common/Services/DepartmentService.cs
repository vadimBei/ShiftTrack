﻿using Microsoft.EntityFrameworkCore;
using ShiftTrack.Core.Application.Data.Common.Interfaces;
using ShiftTrack.Core.Application.Organization.Structure.Common.Interfaces;
using ShiftTrack.Core.Domain.Organization.Structure.Entities;
using ShiftTrack.Kernel.Exceptions;

namespace ShiftTrack.Core.Application.Organization.Structure.Common.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IApplicationDbContext _dbContext;

        public DepartmentService(
            IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Department> GetById(object id, CancellationToken cancellationToken)
        {
            var departmentId = (long)id;

            var department = await _dbContext.Departments
               .AsNoTracking()
               .Include(x => x.Unit)
               .FirstOrDefaultAsync(x => x.Id == departmentId, cancellationToken);

            if (department == null)
                throw new EntityNotFoundException(typeof(Department), departmentId);

            return department;
        }
    }
}

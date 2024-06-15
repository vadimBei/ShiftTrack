﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShiftTrack.Authentication.Extensions;
using ShiftTrack.Core.Application.Data.Common.Interfaces;
using ShiftTrack.Core.Application.Organization.Structure.Common.Interfaces;
using ShiftTrack.Core.Application.Organization.Structure.Common.Services;
using ShiftTrack.Core.Application.Organization.Timesheet.Common.Interfaces;
using ShiftTrack.Core.Application.Organization.Timesheet.Common.Services;
using ShiftTrack.Core.Application.System.User.Common.Interfaces;
using ShiftTrack.Core.Application.System.User.Common.Services;
using ShiftTrack.Core.Infrastructure;
using ShiftTrack.Core.Infrastructure.Repositories.System.User.Employees;
using ShiftTrack.Kernel;
using ShiftTrack.Kernel.Attributes;
using ShiftTrack.WebClient.Http;

namespace ShiftTrack.Core
{
    [ShiftTrackMember]
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddKernel();

            services.AddWebClientHttp(configuration);

            services.AddCurrentUserService();

            services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseNpgsql(
                       configuration.GetConnectionString("DefaultConnection"),
                       b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            //Organization structure
            services.AddTransient<IUnitService, UnitService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IPositionService, PositionService>();

            //Organization timesheet
            services.AddTransient<IShiftService, ShiftService>();

            //System user employees
            services.AddTransient<IEmployeeService, EmployeeService>();

            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();

            return services;
        }
    }
}

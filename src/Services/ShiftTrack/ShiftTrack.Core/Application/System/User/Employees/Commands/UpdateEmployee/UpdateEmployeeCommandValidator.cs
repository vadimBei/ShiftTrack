﻿using FluentValidation;

namespace ShiftTrack.Core.Application.System.User.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Name is required.")
                .Length(1, 64)
                    .WithMessage("Name must be between 1 and 64 characters.");

            RuleFor(x => x.Surname)
                .NotEmpty()
                    .WithMessage("Surname is required.")
                .Length(1, 64)
                    .WithMessage("Surname must be between 1 and 64 characters.");

            RuleFor(x => x.Patronymic)
                .Length(0, 64)
                    .WithMessage("Patronymic can be up to 64 characters long.");

            RuleFor(v => v.Email)
               .NotEmpty()
                   .WithMessage("Email is required")
               .Length(5, 64)
                   .WithMessage("Email lenght min 5, max 64")
               .EmailAddress()
                   .WithMessage("Email is not valid");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                    .WithMessage("PhoneNumber is required.")
                .Matches(@"^\+\d{1,3}\s?\d{1,14}(\s?\d{1,13})?$")
                    .WithMessage("PhoneNumber must be a valid international phone number.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                    .When(x => x.DepartmentId.HasValue)
                        .WithMessage("DepartmentId must be greater than zero.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now)
                    .When(x => x.DateOfBirth.HasValue)
                        .WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.Gender)
                .IsInEnum()
                    .WithMessage("Invalid gender.");
        }
    }
}

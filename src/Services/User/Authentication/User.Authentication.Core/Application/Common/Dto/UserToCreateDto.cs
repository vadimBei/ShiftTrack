﻿namespace User.Authentication.Core.Application.Common.Dto
{
    public record UserToCreateDto(
        string Email,
        string PhoneNumber,
        string Password);
}

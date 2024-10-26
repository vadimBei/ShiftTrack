﻿using FluentAssertions;
using ShiftTrack.Core.Application.Integration.Tests.Abstractions;
using ShiftTrack.Core.Application.Organization.Structure.Positions.Commands.CreatePosition;
using ShiftTrack.Core.Application.Organization.Structure.Positions.Queries.GetPositionById;
using ShiftTrack.Kernel.Exceptions;

namespace ShiftTrack.Core.Application.Integration.Tests.Organization.Structure.Positions.Queries
{
    public class GetPositionByIdQueryTests : BaseIntegrationTest
    {
        public GetPositionByIdQueryTests(
            IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetById_ShouldReturnPosition_WhenPositionExists()
        {
            // Arrange
            var createPositionCommand = new CreatePositionCommand(
                "Адміністратор",
                "Адміністратор магазину");

            var newPosition = await Sender.Send(createPositionCommand);

            var getPositionByIdQuery = new GetPositionByIdQuery(newPosition.Id);

            // Act
            var position = await Sender.Send(getPositionByIdQuery);

            // Assert
            position.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ShouldReturnEntityNotFoundException_WhenPositionNotFound()
        {
            // Arrange
            var getPositionByIdQuery = new GetPositionByIdQuery(1000);

            // Act
            Func<Task> act = async () => await Sender.Send(getPositionByIdQuery);

            // Assert
            await act.Should()
                .ThrowAsync<EntityNotFoundException>();
        }
    }
}

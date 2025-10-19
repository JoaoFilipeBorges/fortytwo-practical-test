using FluentAssertions;
using Fortytwo.PracticalTest.Api.Controllers;
using Fortytwo.PracticalTest.Application.Features.Todos.GetTodos;
using Fortytwo.PracticalTest.Application.ReadModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fortytwo.PracticalTest.Api.UnitTests.Tests;

public class TodoControllerTests
{
    private readonly Mock<IMediator> MediatorMock;
    private readonly TodoController Controller;

    public TodoControllerTests()
    {
        MediatorMock = new Mock<IMediator>();
        Controller = new TodoController(MediatorMock.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnOk_WithTodosList()
    {
        // Arrange
        var expectedResult = new PagedList<TodoDto> {  };
        MediatorMock
            .Setup(m => m.Send(It.IsAny<GetTodosQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await Controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().BeEquivalentTo(expectedResult);

        MediatorMock.Verify(
            m => m.Send(It.IsAny<GetTodosQuery>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
    
}
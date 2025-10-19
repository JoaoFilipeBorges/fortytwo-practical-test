using FluentAssertions;
using FluentValidation;
using Fortytwo.PracticalTest.Application.Features.Todos.CreateTodo;
using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Domain.Common;
using Fortytwo.PracticalTest.Domain.Entities;
using Fortytwo.PracticalTest.Domain.Exceptions;
using Moq;
using User = Fortytwo.PracticalTest.Domain.Entities.User;

namespace Fortytwo.PracticalTest.Application.UnitTests.Tests.Features;

public class TodosTests
{
    private readonly Mock<IUserRepository> UserRepositoryMock;
    private readonly Mock<ITodoRepository> TodoRepositoryMock;
    private readonly CreateTodoCommandHandler CreateTodoCommandHandler;
    private readonly Mock<IValidator<CreateTodoCommand>> ValidatorMock;

    public TodosTests()
    {
        UserRepositoryMock = new Mock<IUserRepository>();
        TodoRepositoryMock = new Mock<ITodoRepository>();
        ValidatorMock = new Mock<IValidator<CreateTodoCommand>>();
        
        ValidatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<CreateTodoCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());
        
        CreateTodoCommandHandler = new CreateTodoCommandHandler(
            UserRepositoryMock.Object,
            TodoRepositoryMock.Object,
            ValidatorMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldCreateTodo_AndReturnTodoId_WhenUserExists()
    {
        // Arrange
        var command = new CreateTodoCommand(
            Title: "Test Todo",
            Description: "Description",
            Done: false,
            DueDate: DateTime.UtcNow.AddDays(1),
            Assignee: 100,
            UserInfo: new RequestUserInfo { UserName = "test" }
        );

        var existingUser = new User
        {
            UserName = "test",
            Email = "test@test.com",
            Id = 42
        };

        UserRepositoryMock
            .Setup(r => r.GetUserIdByUserName(command.UserInfo.UserName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        TodoRepositoryMock
            .Setup(r => r.Create(It.IsAny<Todo>(), It.IsAny<CancellationToken>()))
            .Callback<Todo, CancellationToken>((t, _) => t.Id = 42)
            .Returns(Task.CompletedTask);

        // Act
        var result = await CreateTodoCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(42);
        TodoRepositoryMock.Verify(r => r.Create(It.Is<Todo>(
            t => t.Title == command.Title &&
                 t.Description == command.Description &&
                 t.Done == command.Done &&
                 t.AssigneeId == command.Assignee), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowUserNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new CreateTodoCommand(
            Title: "Test Todo",
            Description: "Description",
            Done: false,
            DueDate: DateTime.UtcNow.AddDays(1),
            Assignee: 100,
            UserInfo: new RequestUserInfo { UserName = "test" }
        );

        UserRepositoryMock
            .Setup(r => r.GetUserIdByUserName(command.UserInfo.UserName, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        Func<Task> act = async () => await CreateTodoCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UserNotFoundException>();

        TodoRepositoryMock.Verify(r => r.Create(It.IsAny<Todo>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
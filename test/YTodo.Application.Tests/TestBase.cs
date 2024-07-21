using AutoFixture;

namespace YTodo.Application.Tests;

public abstract class TestBase
{
    protected Fixture Fixture { get; } = new();
}
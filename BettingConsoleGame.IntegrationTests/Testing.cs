using BettingConsoleGame.Application;
using BettingConsoleGame.Application.Actions;
using BettingConsoleGame.Domain;
using BettingConsoleGame.Domain.Entities;
using BettingConsoleGame.Domain.Services;
using BettingConsoleGame.Domain.ValueObjects;
using BettingConsoleGame.Game;
using BettingConsoleGame.InputHandlers;
using BettingConsoleGame.IntegrationTests.Mocks;
using BettingConsoleGame.OutputHandlers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace BettingConsoleGame.IntegrationTests;

[SetUpFixture]
public class Testing
{
    private static string _consoleReadLineAction = string.Empty;
    private static IServiceScopeFactory _serviceScopeFactory;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        var services = new ServiceCollection();

        services.AddDomain();
        services.AddApplication();
        services.AddConsole();

        ReplaceNumberRandomizerServiceWithMock(services);
        ReplaceConsoleReadLine(services);
        ReplaceConsoleWriteLine(services);
        _serviceScopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
    }

    public static NumberRandomizerServiceMock NumberRandomizerServiceMock { get; private set; } = new NumberRandomizerServiceMock();
    public static Mock<IOutputHandler> WriteLineMock { get; private set; } = new Mock<IOutputHandler>();

    public static Result<IActionResult> ExecuteAction(Wallet wallet, string consoleReadLine)
    {
        _consoleReadLineAction = consoleReadLine;

        using var scope = _serviceScopeFactory.CreateScope();
        var actionHandler = scope.ServiceProvider.GetRequiredService<GameActionHandler>();
        return actionHandler.Execute(wallet);
    }

    public static void SetupNextReadLineAction(string action)
    {
        _consoleReadLineAction = action;
    }

    private static void ReplaceConsoleReadLine(ServiceCollection services)
    {
        var currentUserServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(IInputHandler));

        if (currentUserServiceDescriptor != null)
        {
            services.Remove(currentUserServiceDescriptor);
        }

        // Register testing version
        services.AddTransient(provider => Mock.Of<IInputHandler>(x=> x.ReadLine() == _consoleReadLineAction));
    }

    private static void ReplaceConsoleWriteLine(ServiceCollection services)
    {
        var currentUserServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(IOutputHandler));

        if (currentUserServiceDescriptor != null)
        {
            services.Remove(currentUserServiceDescriptor);
        }

        // Register testing version
        services.AddTransient(provider => WriteLineMock.Object);
    }

    private static void ReplaceNumberRandomizerServiceWithMock(ServiceCollection services)
    {

        // Replace service registration for INumberRandomizerService
        // Remove existing registration
        var currentUserServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(INumberRandomizerService));

        if (currentUserServiceDescriptor != null)
        {
            services.Remove(currentUserServiceDescriptor);
        }

        // Register testing version
        services.AddTransient(provider => NumberRandomizerServiceMock.Mock.Object);
    }

    public static void ResetMocks()
    {
        WriteLineMock = new Mock<IOutputHandler>();
        NumberRandomizerServiceMock = new NumberRandomizerServiceMock();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}

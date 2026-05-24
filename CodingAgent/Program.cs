using AiCodingAgent.Configuration;
using AiCodingAgent.Kernel;
using AiCodingAgent.Agent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Spectre.Console;
using AiCodingAgent.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Configuration/appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var appSettings = configuration.Get<AppSettings>() ?? throw new InvalidOperationException("Failed to load AppSettings from appsettings.json");

        Services appServices = new Services(appSettings);

        // FIX: Inject the initialized appServices instance into your loop handler
        UserInput programLoop = new UserInput(appServices);
        await programLoop.StartLoopAsync();
    }
}


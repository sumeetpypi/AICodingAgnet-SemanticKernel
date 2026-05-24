using AiCodingAgent.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using static System.Net.WebRequestMethods;

namespace AiCodingAgent.Kernel;


/// Responsible for constructing a fully-configured Semantic Kernel instance.
/// All plugin registrations happen here via KernelExtensions.

public static class KernelFactory
{

    /// Builds the kernel from app settings.

    public static Microsoft.SemanticKernel.Kernel Create(
        AppSettings settings,
        ILoggerFactory? loggerFactory = null)
    {
        //ValidateSettings(settings);

        //Console.WriteLine(settings.Endpoint);


        var endpoint = settings.Endpoint ?? new Uri("http://127.0.0.1:8080/v1");

        var httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(10) };
        var builder = Microsoft.SemanticKernel.Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion(
            modelId: "gemma 4",
            endpoint: endpoint,
            apiKey: "dummy-key",
            httpClient: httpClient
            );



        // ── Logging (optional but very useful during development) ─────────────
        if (loggerFactory is not null)
            builder.Services.AddSingleton(loggerFactory);

        // ── Build kernel ──────────────────────────────────────────────────────
        var kernel = builder.Build();

        // ── Register all plugins ──────────────────────────────────────────────
        kernel.RegisterAllPlugins();

        return kernel;
    }
}
    
namespace AiCodingAgent.Services;

using AiCodingAgent.Configuration;
using AiCodingAgent.Kernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Spectre.Console;

public class Services
{
    private readonly ServiceCollection _services = new ServiceCollection();
    public IServiceProvider ServiceProvider { get; }
    public Kernel Kernel { get; }
    public IChatCompletionService CodeService { get; }

    public Services(AppSettings settings)
    {
        // Ensure required services (Kernel, IChatCompletionService, etc.) are registered before building
        Kernel builtKernel = KernelFactory.Create(settings);
        _services.AddSingleton<Kernel>(builtKernel);
   
        // Debugging line to check the value of userinput
        this.ServiceProvider = _services.BuildServiceProvider();
        this.Kernel = this.ServiceProvider.GetRequiredService<Kernel>();
        this.Kernel.RegisterAllPlugins();
        this.CodeService = this.Kernel.GetRequiredService<IChatCompletionService>();
    }
}
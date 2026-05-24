// Extension methods for plugin registration
using System;
using System.IO;
using Microsoft.SemanticKernel;

namespace AiCodingAgent.Kernel;

public static class KernelExtensions
{

    /// Registers all built-in plugins from the Plugins directory.

    public static KernelPlugin CodingPlugin { get; private set; }
    
    public static KernelFunction CodeFunction { get; private set; }
    public static KernelPlugin DockerPlugin { get; private set; }
    public static KernelFunction DockerFunction { get; private set; }


    public static void RegisterAllPlugins(this Microsoft.SemanticKernel.Kernel kernel)
    {
        const string pluginName = "CodingPlugin";
        const string pluginDocker = "DockerPlugin";

        // Check if plugin is already registered
        if (kernel.Plugins.Contains(pluginName) || kernel.Plugins.Contains(pluginDocker))
        {
            var existingPlugin = kernel.Plugins[pluginName];
            var existingDockerPlugin = kernel.Plugins[pluginDocker];
            CodeFunction  = existingPlugin["Code"];
            DockerFunction = existingDockerPlugin["docker"];
            //Console.WriteLine($"Plugin '{pluginName}' already registered. Reused existing reference.");
            return;
        }

        // Load plugin from directory
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var projectRoot = baseDirectory.Split(
            new[] { $"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}" },
            StringSplitOptions.None
        )[0];

        var codingPluginDirectoryPath = Path.Combine(projectRoot, "Plugins", "CodingPlugin");
        var codingPluginDockerDirectoryPath = Path.Combine(projectRoot, "Plugins", "DockerPlugin");
        //Console.WriteLine($"Loading CodingPlugin from: {codingPluginDirectoryPath}");
        //Console.WriteLine($"Loading DockerPlugin from: {codingPluginDockerDirectoryPath}");

        if (!Directory.Exists(codingPluginDirectoryPath) && !Directory.Exists(codingPluginDockerDirectoryPath))
        {
            throw new DirectoryNotFoundException(
                $"Could not find CodingPlugin directory at: {codingPluginDirectoryPath}"
            );
        }


        // Import and store the plugin
        var CodingPlugin = kernel.ImportPluginFromPromptDirectory(codingPluginDirectoryPath);
        var DockerPlugin = kernel.ImportPluginFromPromptDirectory(codingPluginDockerDirectoryPath);
        CodeFunction = CodingPlugin["Code"];
        DockerFunction = DockerPlugin["Docker"];

        //Console.WriteLine($"Plugin '{pluginName} {pluginDocker}' successfully functions.");
    }
}

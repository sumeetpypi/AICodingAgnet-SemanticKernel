namespace AiCodingAgent.Agent;

using AiCodingAgent.Configuration;
using AiCodingAgent.Kernel;
using AiCodingAgent.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Spectre.Console;
using System.Text;
using System.Reflection;


//executionSettings: new OpenAIPromptExecutionSettings
//                {
//    //MaxTokens = settings.Agent.MaxTokens,
//    //Temperature = settings.Agent.Temperature
//    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
//},

public class CodingAgent
{
    public async Task StartAsync()
    {
        AnsiConsole.MarkupLine("[bold cyan]Paste your code and press Ctrl+Z then Enter (Windows)[/]");
        //AnsiConsole.MarkupLine("[grey]For Linux/Mac use Ctrl+D[/]\n");

        string userCode = Console.In.ReadToEnd();

        if (string.IsNullOrWhiteSpace(userCode))
        {
            AnsiConsole.MarkupLine("[bold red]No code provided.[/]");
            return;
        }

        //AnsiConsole.MarkupLine("[grey]Processing your code...[/]\n");

        await RunAsync(userCode);
    }

    private static string CleanResponse(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        return text
            .Replace("<|im_start|>", "")
            .Replace("<|im_end|>", "")
            .Replace("<start_of_turn>", "")
            .Replace("<end_of_turn>", "")
            .Trim();
    }
    public async Task RunAsync(string userCode)
    {
        try
        {
            var fullReply = new StringBuilder();

            AppSettings settings = new AppSettings();
            Services appServices = new Services(settings);
            CodeParserService codeParser = new CodeParserService(appServices);

            var chatHistory = new ChatHistory();
            chatHistory.AddSystemMessage("@\"You are an expert coding assistant." +
                "Return only the final answer." +
                "Do not output chat template tokens " +
                "Do not output <|im_start|> or <|im_end|>." +
                "Do not include role names.\""
                );
           

            chatHistory.AddUserMessage(userCode);

            bool hasReceivedContent = false;

            await foreach (var chunk in codeParser._CodeService.GetStreamingChatMessageContentsAsync(
                chatHistory,
                executionSettings: new OpenAIPromptExecutionSettings
                {
                    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
                },
                kernel: appServices.Kernel
            ))
            {
                if (chunk == null) continue;

                string content = null;

                if (!string.IsNullOrEmpty(chunk.Content))
                {
                    content = chunk.Content;
                }
               
                else if (chunk.InnerContent != null)
                {
                   
                    var innerContent = chunk.InnerContent;

                  
                    var deltaProperty = innerContent.GetType().GetProperty("Delta");
                    if (deltaProperty != null)
                    {
                        var delta = deltaProperty.GetValue(innerContent);
                        if (delta != null)
                        {
                            var contentProperty = delta.GetType().GetProperty("Content");
                            if (contentProperty != null)
                            {
                                content = contentProperty.GetValue(delta)?.ToString();
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(content))
                {
                    //Console.Write(content);
                    AnsiConsole.Markup($"[white]{Markup.Escape(chunk.Content ?? "")}[/]");
                    fullReply.Append(content);
                    hasReceivedContent = true;
                }
            }

            if (!hasReceivedContent)
            {
                AnsiConsole.MarkupLine("\n[bold yellow]Warning:[/] No content received from the AI model.");
            }
            else
            {
                chatHistory.AddAssistantMessage(fullReply.ToString());
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[bold red]Error:[/] {ex.Message}");
            //AnsiConsole.MarkupLine($"\n[grey]Stack Trace:[/]\n{ex.StackTrace}");
        }
    }
}
using AiCodingAgent.Agent;
using AiCodingAgent.Configuration;
using AiCodingAgent.Services;
using Spectre.Console;
using System;
using System.Threading.Tasks;

public class UserInput
{
    private readonly Services _appServices;

    public UserInput(Services appServices)
    {
        _appServices = appServices;
    }

    public async Task StartLoopAsync()
    {
        AgentPrompts prompts = new AgentPrompts();
        prompts.DisplayConsoleCommands();
        AppSettings settings = new AppSettings();   

        while (true)
        {
            AnsiConsole.Markup("\n[bold green]You:[/] ");     

            
            var userInput = Console.ReadLine()?.Trim();; // Debugging line to check the value of userinput

            if (string.IsNullOrWhiteSpace(userInput)) continue;

            if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                AnsiConsole.MarkupLine("\n[grey]Goodbye! Happy coding.[/]");
                break;
            }

            try
            {
                var parts = userInput.Split(' ', 2);
                string command = parts[0].ToLower();
                string argument = parts.Length > 1 ? parts[1] : string.Empty;

                string[] supportedCommands = { "Debug Code", "1", "Docker Commands" ,"2" };

                string matchedCommand = supportedCommands.FirstOrDefault(cmd => cmd.Equals(command, StringComparison.OrdinalIgnoreCase));

                if (matchedCommand != null)
                {
                    await HandleCommand(matchedCommand);
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]Invalid command.[/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }
        }
    }

    public async Task HandleCommand(string matchedCommand)
    {
        if (matchedCommand == "Debug Code" || matchedCommand == "1")
        {
            CodingAgent codingAgent = new CodingAgent();
            await codingAgent.StartAsync();
        }
        if (matchedCommand == "Docker Commands" || matchedCommand == "2")
        {
            CodingAgent codingAgent = new CodingAgent();
            await codingAgent.StartAsync();
        }
        
    }
}
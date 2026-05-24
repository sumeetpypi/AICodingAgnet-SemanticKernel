namespace AiCodingAgent.Agent;

using Spectre.Console;

public class AgentPrompts
{
    // This is the constructor
    public AgentPrompts()
    {
        // Executable code must live inside a method or constructor block
        AnsiConsole.Write(
            new FigletText("AI Coding Agent")
                .Centered()
                .Color(Color.Red)
        );
    }

    public void DisplayConsoleCommands()
    {
        AnsiConsole.MarkupLine("[bold cyan]Multi-Agent Developer Assistant[/]");
        AnsiConsole.MarkupLine("[grey]Supports:[/]");
        AnsiConsole.MarkupLine("[red]- Code debugging[/]");
        AnsiConsole.MarkupLine("[red]- Docker commands[/]");
        AnsiConsole.MarkupLine("[red]- Code generation[/]");
        AnsiConsole.MarkupLine("[red]- Error explanations[/]");
        AnsiConsole.MarkupLine("[red]- DevOps assistance[/]");
        //AnsiConsole.MarkupLine("\n[red]Ask anything naturally.[/]");
        
        AnsiConsole.MarkupLine("\n[bold yellow]Available Commands:[/]");

        //AnsiConsole.MarkupLine("[green]1.[/] [blue]Generate Code[/] - Generate code based on a prompt.");
        //AnsiConsole.MarkupLine("[green]2.[/] [blue]Explain Code[/] - Get an explanation of existing code.");
        //AnsiConsole.MarkupLine("[green]3.[/] [blue]Refactor Code[/] - Improve the structure of existing code.");
        AnsiConsole.MarkupLine("[green]1.[/] [blue]Debug Code[/] - Identify and fix issues in code.");
        AnsiConsole.MarkupLine("[green]2.[/] [blue]Docker Commands[/] - Get an explanation of docker commands and generate commands");
        AnsiConsole.MarkupLine("[red]Type 'exit' to quit.[/]\n");
    }
}
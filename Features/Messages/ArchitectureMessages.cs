using Spectre.Console;

namespace ScaffNetConsole.Features.Messages
{
    internal static class ArchitectureMessages
    {
        internal static void ShowScaffoldingMessage(ShowScaffoldingMessageDto dto)
        {
            AnsiConsole.MarkupLine($"[green]Scaffolding {dto.ArchitectureName}...[/]");
            AnsiConsole.MarkupLine($"Project Name: [yellow]{dto.ProjectName}[/]");
            AnsiConsole.MarkupLine($"Project Path: [blue]{dto.ProjectPath}[/]");
        }

    }
}

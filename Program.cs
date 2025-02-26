using ScaffNet.Features.CleanArchitecture;
using ScaffNet.Utils.ErrorHandling;
using ScaffNet.Utils;
using System.Threading.Tasks;
using ScaffNetConsole;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;

using Constants = ScaffNet.Data.Constants;

class Program
{
    static int Main(string[] args)
    {
        Logger.SetupScaffLogger();

        var app = new CommandApp();

        app.Configure(config =>
        {
            config.SetApplicationName("ScaffNetConsole");
            config.SetApplicationVersion("1.0.0");

            config.AddCommand<ScaffoldCommand>("arch-cl")
                  .WithDescription("Scaffold a Clean Architecture project");
        });


#if DEBUG
        RunAppInALoop(app);
        return 0;
#else
        return app.Run(args);
#endif
    }

    private static void RunAppInALoop(CommandApp app)
    {
        while (true)
        {
            var input = AnsiConsole.Ask<string>("[blue]Enter command (or 'exit' to quit):[/]");

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                AnsiConsole.MarkupLine("[red]Exiting...[/]");
                break;
            }

            try
            {
                app.Run(input.Split(' '));
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
        }
    }
}

public class ScaffoldCommand : Command<ScaffoldCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("-n|--name")]
        [Description("Project name (default: CleanArchitecture)")]
        [DefaultValue("CleanArchitecture")]
        public string Name { get; set; }

        [CommandOption("-p|--path")]
        [Description("Project path (default: C:/CleanArchitecture)")]
        [DefaultValue("C:/CleanArchitecture")]
        public string Path { get; set; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.MarkupLine("[green]Scaffolding Clean Architecture...[/]");
        AnsiConsole.MarkupLine($"Project Name: [yellow]{settings.Name}[/]");
        AnsiConsole.MarkupLine($"Project Path: [blue]{settings.Path}[/]");

        CleanArchitectureScaffolder.Create(new CleanArchitectureArgs
        {
            SolutionName = settings.Name,
            SolutionPath = settings.Path,
            SourceFolder = "src"
        });

        return 0;
    }
}
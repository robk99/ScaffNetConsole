using ScaffNetConsole;
using ScaffNetConsole.Architectures.CleanArchitecture;
using Spectre.Console;
using Spectre.Console.Cli;

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

            config.AddCommand<CleanArchitectureCommand>("arch-cl")
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

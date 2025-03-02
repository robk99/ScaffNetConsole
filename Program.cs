using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScaffNet.Utils;
using ScaffNetConsole.Features.CleanArchitecture;
using Spectre.Console;
using Spectre.Console.Cli;

class Program
{
    private static string _environment;


    static int Main(string[] args)
    {
#if DEBUG
        _environment = "Development";
#else
_environment = "Production";
#endif

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{_environment}.json", optional: true, reloadOnChange: true)
            .Build();

        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
            .AddConfiguration(config.GetSection("Logging"))
            .AddConsole();
        });

        var logger = loggerFactory.CreateLogger<Program>();

        ScaffUtils.SetEventHandler(new ScaffNetConsole.Utils.EventHandler(logger));

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

using ScaffNet.Features.CleanArchitecture;
using ScaffNet.Utils.ErrorHandling;
using ScaffNet.Utils;
using System.Threading.Tasks;

using Constants = ScaffNet.Data.Constants;
using ScaffNetConsole;
using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;


class Program
{
    static int Main(string[] args)
    {
        Logger.SetupScaffLogger();

        var app = new CommandApp();

        // Set up custom help output
        app.Configure(config =>
        {
            config.SetApplicationName("ScaffNetConsole");
            config.SetApplicationVersion("1.0.0");

            config.AddCommand<ScaffoldCommand>("arch-cl")
                  .WithDescription("Scaffold a Clean Architecture project");

            config.AddCommand<HelpCommand>("help")
                  .WithDescription("Show available commands");
        });

        return app.Run(args);
    }
}

// 🏗️ Define the Scaffold Command
public class ScaffoldCommand : Command<ScaffoldCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Command>")]
        [Description("The command to execute (e.g., 'arch-cl')")]
        public string Command { get; set; }

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
        if (settings.Command == "arch-cl")
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
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Error: Unknown command.[/]");
        }

        return 0;
    }
}

// 🆘 Define a Custom Help Command
public class HelpCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.WriteLine("Available commands:");
        var table = new Table();
        table.AddColumn("Command");
        table.AddColumn("Description");

        table.AddRow("[green]arch-cl[/]", "Scaffold a Clean Architecture project");
        table.AddRow("[green]help[/]", "Show this help menu");

        AnsiConsole.Write(table);
        return 0;
    }
}

//internal class Program
//{
//    private static void Main(string[] args)
//    {
//        try
//        {
//            if (args.Length == 0)
//            {
//                Console.WriteLine("No arguments provided");
//                return;
//            }

//            Logger.SetupScaffLogger();

//            if (args[0] == "arch-cl")
//            {
//                var solutonName = args.Length > 1 ? args[1] : Constants.SOLUTION_NAME;
//                var solutionPath = args.Length > 2 ? args[2] : Constants.SOLUTION_PATH;

//                CleanArchitectureScaffolder.Create(new CleanArchitectureArgs
//                {
//                    SolutionName = solutonName,
//                    SolutionPath = solutionPath,
//                    SourceFolder = "src"
//                });
//            }
//            else
//            {
//                Console.WriteLine("Wrong argument.");
//            }
//        }
//        catch (ScaffNetCommandException ex)
//        {
//            // clients will handle this further
//            Console.WriteLine();
//        }
//        catch (Exception ex)
//        {
//            ScaffLogger.Default.LogError("Unhandled exception: " + ex.Message);
//            throw new Exception("Unhandled error happened!");
//            // clients will handle this further
//        }
//    }
//}
using ScaffNet.Features.CleanArchitecture;
using ScaffNetConsole.Architectures.Messages;
using Spectre.Console.Cli;

namespace ScaffNetConsole.Architectures.CleanArchitecture
{
    internal class CleanArchitectureCommand : Command<CleanArchitectureSettings>
    {
        public override int Execute(CommandContext context, CleanArchitectureSettings settings)
        {
            ArchitectureMessages.ShowScaffoldingMessage(new()
            {
                ArchitectureName = "Clean Architecture",
                ProjectName = settings.Name,
                ProjectPath = settings.Path
            });

            CleanArchitectureScaffolder.Create(new CleanArchitectureArgs
            {
                SolutionName = settings.Name,
                SolutionPath = settings.Path,
                SourceFolder = "src"
            });

            return 0;
        }
    }
}

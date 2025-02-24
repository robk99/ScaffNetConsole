using ScaffNet.Features.CleanArchitecture;
using ScaffNet.Utils.ErrorHandling;
using ScaffNet.Utils;

using Constants = ScaffNet.Data.Constants;
using ScaffNetConsole;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments provided");
                return;
            }

            Logger.SetupScaffLogger();

            if (args[0] == "arch-cl")
            {
                var solutonName = args.Length > 1 ? args[1] : Constants.SOLUTION_NAME;
                var solutionPath = args.Length > 2 ? args[2] : Constants.SOLUTION_PATH;

                CleanArchitectureScaffolder.Create(new CleanArchitectureArgs
                {
                    SolutionName = solutonName,
                    SolutionPath = solutionPath,
                    SourceFolder = "src"
                });
            }
            else
            {
                Console.WriteLine("Wrong argument.");
            }
        }
        catch (ScaffNetCommandException ex)
        {
            // clients will handle this further
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            ScaffLogger.Default.LogError("Unhandled exception: " + ex.Message);
            throw new Exception("Unhandled error happened!");
            // clients will handle this further
        }
    }
}
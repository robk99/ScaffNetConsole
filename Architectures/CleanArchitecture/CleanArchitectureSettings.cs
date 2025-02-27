using Spectre.Console.Cli;
using System.ComponentModel;

namespace ScaffNetConsole.Architectures.CleanArchitecture
{
    internal class CleanArchitectureSettings : CommandSettings, IArchitectureSetting
    {
        [CommandOption("-n|--name")]
        [Description("Project name")]
        [DefaultValue("CleanArchitecture")]
        public string Name { get; set; }

        [CommandOption("-p|--path")]
        [Description("Project path")]
        [DefaultValue("CleanArchitecture")]
        public string Path { get; set; }
    }
}

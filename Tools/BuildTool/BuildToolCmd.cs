using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Threading.Tasks;

using BuildTool.SubCommand.Adf;

namespace BuildTool
{
    [Command(Name = "BuildTool", OptionsComparison = System.StringComparison.InvariantCultureIgnoreCase)]
    [VersionOptionFromMember("--version", MemberName = nameof(GetVersion))]
    [Subcommand(typeof(AdfUpdateTrigger))]
    class BuildToolCmd
    {
        protected ILogger _logger;
        protected IConsole _console;

        public BuildToolCmd(ILogger<BuildToolCmd> logger, IConsole console)
        {
            _logger = logger;
            _console = console;
        }

        protected Task<int> OnExecute(CommandLineApplication app)
        {
            // this shows help even if the --help option isn't specified
            _logger.LogInformation("Executing BuildTools utility");
            app.ShowHelp();
            return Task.FromResult(0);
        }

        private static string GetVersion()
            => typeof(BuildToolCmd).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}
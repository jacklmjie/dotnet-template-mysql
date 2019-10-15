using McMaster.Extensions.CommandLineUtils;
using System.Threading.Tasks;
using System.Reflection;

namespace dotnetmysql
{
    [Command(Name = "dotnetmysql", Description = "DotnetMysql is a .NET Core Global Tool.Dotnet MySql Tool can help you convert SqlServer Table and Proc to MySql")]
    [Subcommand(typeof(ConvertTable))]
    [Subcommand(typeof(ConvertProc))]
    [VersionOptionFromMember("-v|--version", MemberName = nameof(GetVersion))]
    class Program
    {
        static async Task Main(string[] args)
        {
            await CommandLineApplication.ExecuteAsync<Program>(args);
        }

        private int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();
            return 0;
        }

        private static string GetVersion()
           => typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}

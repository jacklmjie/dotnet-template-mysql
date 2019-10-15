using System.Reflection;

namespace dotnetmysql.Extensions
{
    public class SystemExtensions
    {
        public static string GetVersion()
         => typeof(SystemExtensions).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}

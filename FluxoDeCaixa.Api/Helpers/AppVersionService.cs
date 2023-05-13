using System.Reflection;

namespace FluxoDeCaixa.Api.Helpers
{
    public static class AppVersionService
    {
        public static string Version => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
    }
}

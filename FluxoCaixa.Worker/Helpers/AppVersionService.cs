using System.Reflection;
namespace FluxoCaixa.Worker.Helpers
{
    public static class AppVersionService
    {
        public static string Version => Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion;
    }
}

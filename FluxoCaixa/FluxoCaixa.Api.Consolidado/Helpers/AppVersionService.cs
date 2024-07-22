﻿using System.Reflection;
namespace FluxoCaixa.Api.Consolidado.Helpers
{
    public static class AppVersionService
    {
        public static string Version => Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion;
    }
}

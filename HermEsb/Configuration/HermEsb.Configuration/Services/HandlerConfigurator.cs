using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace HermEsb.Configuration.Services
{
    public static class HandlerConfigurator
    {
        private static IEnumerable<Type> _typesToSubscribe;

        /// <summary>
        /// Scans the assemblies with handlers.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns></returns>
        public static ConfigurationHelper ScanAssembliesWithHandlers(this ConfigurationHelper config)
        {
            return config.ScanAssembliesWithHandlers(AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>
        /// Scans the assemblies with handlers.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static ConfigurationHelper ScanAssembliesWithHandlers(this ConfigurationHelper config, string path)
        {
            return config.GetTypesOfHandlers(GetAssembliesInDirectory(path));
        }

        /// <summary>
        /// Gets the types of handlers.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <param name="assemblies">The assemblies.</param>
        /// <returns></returns>
        public static ConfigurationHelper GetTypesOfHandlers(this ConfigurationHelper config, IEnumerable<Assembly> assemblies)
        {
            _typesToSubscribe = assemblies.SelectMany(asm => asm.GetTypes().Select(t => t)).ToList();
            return config;
        }

        /// <summary>
        /// Gets the assemblies in directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="assembliesToSkip">The assemblies to skip.</param>
        /// <returns></returns>
        private static IEnumerable<Assembly> GetAssembliesInDirectory(string path, params string[] assembliesToSkip)
        {
            foreach (var a in GetAssembliesInDirectoryWithExtension(path, "*.exe", assembliesToSkip))
                yield return a;
            foreach (var a in GetAssembliesInDirectoryWithExtension(path, "*.dll", assembliesToSkip))
                yield return a;
        }

        /// <summary>
        /// Gets the assemblies in directory with extension.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="assembliesToSkip">The assemblies to skip.</param>
        /// <returns></returns>
        private static IEnumerable<Assembly> GetAssembliesInDirectoryWithExtension(string path, string extension, params string[] assembliesToSkip)
        {
            var result = new List<Assembly>();
            foreach (var file in new DirectoryInfo(path).GetFiles(extension, SearchOption.AllDirectories))
            {
                try
                {
                    if (assembliesToSkip.Contains(file.Name, StringComparer.InvariantCultureIgnoreCase))
                        continue;

                    result.Add(Assembly.LoadFrom(file.FullName));
                }
                catch (BadImageFormatException bif)
                {
                    if (bif.FileName.ToLower().Contains("system.data.sqlite.dll"))
                        throw new BadImageFormatException(
                            "You've installed the wrong version of System.Data.SQLite.dll on this machine. If this machine is x86, this dll should be roughly 800KB. If this machine is x64, this dll should be roughly 1MB. You can find the x86 file under /binaries and the x64 version under /binaries/x64. *If you're running the samples, a quick fix would be to copy the file from /binaries/x64 over the file in /binaries - you should 'clean' your solution and rebuild after.",
                            bif.FileName, bif);

                    throw new InvalidOperationException(
                        "Could not load " + file.FullName +
                        ". Consider using 'Configure.With(AllAssemblies.Except(\"" + file.Name + "\"))' to tell NServiceBus not to load this file.",
                        bif);
                }
            }
            return result;
        }
    }
}
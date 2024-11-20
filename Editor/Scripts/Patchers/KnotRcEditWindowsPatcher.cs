using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Knot.Core;
using UnityEditor.Compilation;

namespace Knot.RCPatcher.Editor
{
    [Serializable]
    [KnotTypeInfo("RCEdit Windows Patcher")]
    public class KnotRcEditWindowsPatcher : IKnotRcPatcher
    {
        protected const string RcEditExecutableLocalPath = "Plugins/rcedit-win/rcedit-x64.exe";
        protected static readonly string[] TargetFileExtensions = { "exe", "dll" };


        public virtual Task<KnotRcPatcherResult> Patch(IEnumerable<string> filePaths, IEnumerable<KeyValuePair<string, string>> properties, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                return Task.FromResult(KnotRcPatcherResult.Empty);

            var rcEditFileInfo = new FileInfo(GetRcEditExecutablePath());
            if (!rcEditFileInfo.Exists)
                throw new FileNotFoundException("Could not find rcedit executable", rcEditFileInfo.FullName);

            List<string> filesPatched = new();
            foreach (var filePath in filePaths)
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    var startInfo = new ProcessStartInfo(rcEditFileInfo.FullName);

                    var propertiesSb = new StringBuilder();
                    foreach (var property in properties)
                    {
                        switch (property.Key)
                        {
                            default:
                                propertiesSb.Append($" --set-version-string \"{property.Key}\" \"{property.Value}\"");
                                break;
                            case "FileVersion":
                                propertiesSb.Append($" --set-file-version \"{property.Value}\"");
                                break;
                        }
                    }

                    var fileInfo = new FileInfo(filePath);
                    startInfo.WorkingDirectory = fileInfo.DirectoryName;
                    startInfo.Arguments = $"{fileInfo.Name} {propertiesSb}";
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.CreateNoWindow = true;
                    startInfo.UseShellExecute = false;

                    using var process = Process.Start(startInfo);
                    var error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    
                    if (!string.IsNullOrEmpty(error))
                        throw new Exception(error);

                    filesPatched.Add(fileInfo.FullName);
                }
            }
            
            return Task.FromResult(new KnotRcPatcherResult
            {
                FilesPatched = filesPatched
            });
        }

        public IEnumerable<string> GetTargetFileExtensions() => TargetFileExtensions;


        protected virtual string GetRcEditExecutablePath()
        {
            var assemblyPath = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName(typeof(KnotRcPatcher).Namespace);
            var packageRootPath = Path.GetDirectoryName(assemblyPath);
            return string.IsNullOrEmpty(packageRootPath) ? string.Empty : Path.Combine(packageRootPath, RcEditExecutableLocalPath);
        }
    }
}

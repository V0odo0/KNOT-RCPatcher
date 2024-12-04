using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Knot.Core;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Knot.RCPatcher.Editor
{
    public class KnotRcPatcher : IPostprocessBuildWithReport
    {
        internal const string CoreName = "RC Patcher";
        internal const string CorePath = "KNOT/" + CoreName + "/";

        public static KnotRcPatcherProjectSettings ProjectSettings =>
            _projectSettings == null ? _projectSettings = Utils.GetProjectSettings<KnotRcPatcherProjectSettings>(false) : _projectSettings;
        private static KnotRcPatcherProjectSettings _projectSettings;

        public int callbackOrder => ProjectSettings.BuildPostProcessCallbackOrder;
        

        public void OnPostprocessBuild(BuildReport report)
        {
            if (!ProjectSettings.PatchOnBuildPostProcess)
                return;

            if (report.summary.result is BuildResult.Failed or BuildResult.Cancelled)
                return;

            foreach (var profile in ProjectSettings.BuildPostProcessors)
            {
                if (!profile.Enabled)
                    continue;

                if (profile.PatcherProfile == null)
                    continue;

                if (!profile.TargetFiles.Any())
                    continue;

                var targetFiles = new List<string>();
                foreach (var reportFile in report.files)
                {
                    if (!File.Exists(reportFile.path))
                        continue;

                    if (profile.TargetFiles.Any(s => reportFile.path.EndsWith(s, StringComparison.CurrentCultureIgnoreCase)))
                        targetFiles.Add(reportFile.path);
                }

                var resultTask = profile.PatcherProfile.Patcher.Patch(targetFiles,
                    profile.PatcherProfile.PropertyProvider.GetProperties());

                if (resultTask.IsCanceled)
                    Debug.LogWarning($"{CoreName}: Patching was cancelled");
                else if (!resultTask.Result.FilesPatched.Any())
                    Debug.Log($"{CoreName}: No files patched");
                else
                {
                    StringBuilder reportSb = new StringBuilder();
                    reportSb.AppendLine($"{CoreName}: Patched files:");
                    foreach (var patchedFile in resultTask.Result.FilesPatched)
                        reportSb.AppendLine(patchedFile);

                    Debug.Log(reportSb);
                }
            }
        }
    }
}

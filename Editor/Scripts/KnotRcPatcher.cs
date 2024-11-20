using System;
using System.Collections.Generic;
using System.Linq;
using Knot.Core;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Knot.RCPatcher.Editor
{
    public class KnotRcPatcher : IPostprocessBuildWithReport
    {
        internal const string CoreName = "RC Patcher";
        internal const string CorePath = "KNOT/" + CoreName + "/";

        public static KnotRcPatcherProjectSettings ProjectSettings =>
            _projectSettings == null ? _projectSettings = Utils.GetProjectSettings<KnotRcPatcherProjectSettings>() : _projectSettings;
        private static KnotRcPatcherProjectSettings _projectSettings;

        public int callbackOrder => ProjectSettings.BuildPostProcessCallbackOrder;
        

        public void OnPostprocessBuild(BuildReport report)
        {
            if (!ProjectSettings.PatchOnBuildPostProcess)
                return;

            if (report.summary.result == BuildResult.Failed || report.summary.result == BuildResult.Cancelled)
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
                    if (profile.TargetFiles.Any(s => reportFile.path.EndsWith(s, StringComparison.CurrentCultureIgnoreCase)))
                        targetFiles.Add(reportFile.path);
                }

                var resultTask = profile.PatcherProfile.Patcher.Patch(targetFiles,
                    profile.PatcherProfile.PropertyProvider.GetProperties());
                resultTask.RunSynchronously();
            }
        }
    }
}

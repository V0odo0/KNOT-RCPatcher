using System.Collections;
using System.Collections.Generic;
using Knot.Core;
using UnityEngine;

namespace Knot.RCPatcher.Editor
{
    public static class KnotRcPatcher
    {
        public const string CorePath = "KNOT/RC Patcher/";

        internal static KnotRcPatcherProjectSettings ProjectSettings =>
            _projectSettings == null ? _projectSettings = Utils.GetProjectSettings<KnotRcPatcherProjectSettings>() : _projectSettings;
        private static KnotRcPatcherProjectSettings _projectSettings;
    }
}

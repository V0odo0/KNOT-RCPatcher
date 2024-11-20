using System;
using System.Collections.Generic;
using Knot.Core;
using UnityEngine;

namespace Knot.RCPatcher.Editor
{
    public class KnotRcPatcherProjectSettings : ScriptableObject
    {
        [field: SerializeField] public bool PatchOnBuildPostProcess { get; set; }
        [field: SerializeField] public int BuildPostProcessCallbackOrder { get; set; } = 100;
        [field: SerializeField] public List<BuildPostProcessor> BuildPostProcessors { get; set; } = new()
        {
            new BuildPostProcessor()
        };


        [Serializable]
        public class BuildPostProcessor
        {
            [field: SerializeField] public bool Enabled { get; set; } = true;
            [field: SerializeField] public List<string> TargetFiles { get; set; } = new()
            {
                ".exe", "Assembly-CSharp.dll", "GameAssembly.dll"
            };
            [field: SerializeField, KnotCreateAssetField(typeof(KnotRcPatcherProfile))]
            public KnotRcPatcherProfile PatcherProfile { get; set; }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Knot.Core.Editor;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace Knot.RCPatcher.Editor
{
    [CustomEditor(typeof(KnotRcPatcherProjectSettings))]
    internal class KnotRcPatcherProjectSettingsEditor : ProjectSettingsEditor<KnotRcPatcherProjectSettings>
    {
        internal static string SettingsPath = $"Project/KNOT/RC Patcher";

        [SettingsProvider]
        static SettingsProvider GetSettingsProvider()
        {
            return GetSettingsProvider(KnotRcPatcher.ProjectSettings, SettingsPath,
                typeof(KnotRcPatcherProjectSettingsEditor));
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            return;

            serializedObject.Update();

            var customSettings = serializedObject.FindProperty("_customProfile");
            EditorGUILayout.PropertyField(customSettings);
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space(10);

            if (customSettings.objectReferenceValue == null)
                base.OnInspectorGUI();
        }

        [MenuItem("Tools/Foo")]
        public static void Foo()
        {
            var pa = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName("Knot.RCPatcher.Editor");
            Debug.Log(pa);
        }
    }
}

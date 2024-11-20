using Knot.Core.Editor;
using UnityEditor;

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
            serializedObject.Update();
            
            EditorGUIUtility.labelWidth = 220;
            
            EditorGUILayout.PropertyField(serializedObject.FindBackingFieldProperty(nameof(Target.PatchOnBuildPostProcess)));
            if (Target.PatchOnBuildPostProcess)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(serializedObject.FindBackingFieldProperty(nameof(Target.BuildPostProcessCallbackOrder)));
                EditorGUILayout.PropertyField(serializedObject.FindBackingFieldProperty(nameof(Target.BuildPostProcessors)));
                
                EditorGUI.indentLevel--;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}

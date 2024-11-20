using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Knot.RCPatcher.Editor
{
    [CustomEditor(typeof(KnotRcPatcherProfile))]
    public class KnotRcPatcherProfileEditor : UnityEditor.Editor
    {
        private KnotRcPatcherProfile _target;

        protected virtual void OnEnable()
        {
            _target = target as KnotRcPatcherProfile;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(10);
            GUI.enabled = _target.Patcher != null && _target.PropertyProvider != null;

            EditorGUILayout.HelpBox("Warning: Patching will overwrite the file's metadata permanently.", MessageType.Warning);
            if (GUILayout.Button("Patch Selected..."))
            {
                var extensions = string.Join(',', _target.Patcher!.GetTargetFileExtensions().Distinct());
                string path;
                if (extensions.Any())
                    path = EditorUtility.OpenFilePanelWithFilters("Select file to patch", string.Empty,
                        new[] { extensions, extensions });
                else path = EditorUtility.OpenFilePanel("Select file to patch", string.Empty, string.Empty);
                if (!string.IsNullOrEmpty(path))
                {
                    var fileInfo = new FileInfo(path);
                    if (fileInfo.Exists && !fileInfo.IsReadOnly)
                    {
                        var result = _target.Patcher.Patch(new[] { fileInfo.FullName }, _target.PropertyProvider!.GetProperties());
                        Debug.Log(result);
                    }
                }
                
            }
            GUI.enabled = true;
        }
    }
}

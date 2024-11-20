using System.Collections.Generic;
using Knot.Core;
using UnityEngine;

namespace Knot.RCPatcher.Editor
{
    [CreateAssetMenu(fileName = nameof(KnotRcPatcherProfile), menuName = KnotRcPatcher.CorePath + "RC Patcher Profile")]
    public class KnotRcPatcherProfile : ScriptableObject
    {
        public IKnotRcPatcher Patcher => _patcher ?? (_patcher = new KnotRcEditWindowsPatcher());
        [SerializeReference, KnotTypePicker(typeof(IKnotRcPatcher))]
        private IKnotRcPatcher _patcher = new KnotRcEditWindowsPatcher();

        public IKnotRcPatcherPropertyProvider PropertyProvider => _propertyProvider ?? (_propertyProvider = new KnotRcEditWindowsPropertyProvider());
        [SerializeReference, KnotTypePicker(typeof(IKnotRcPatcherPropertyProvider))]
        private IKnotRcPatcherPropertyProvider _propertyProvider = new KnotRcEditWindowsPropertyProvider
        {
            Properties = new List<KnotRcEditWindowsPropertyProvider.Property>
            {
                new ("CompanyName", "<CompanyName>"),
                new ("LegalCopyright", "Copyright <CompanyName>"),
                new ("ProductName", "<ProductName>"),
                new ("ProductVersion", "<ProductVersion> (<BuildGuid>)"),
                new ("FileVersion", "<ProductVersion>")
            }
        };
    }
}

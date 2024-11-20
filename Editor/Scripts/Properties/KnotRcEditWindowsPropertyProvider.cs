using Knot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Knot.RCPatcher.Editor
{
    [Serializable]
    [KnotTypeInfo("RCEdit Windows PropertyProvider")]
    public class KnotRcEditWindowsPropertyProvider : IKnotRcPatcherPropertyProvider
    {
        [field: SerializeField] public List<Property> Properties { get; set; } = new();

        public IEnumerable<KeyValuePair<string, string>> GetProperties() => 
            Properties.Select(p => new KeyValuePair<string, string>(p.Name, FormatPropertyValue(p.Value)));


        public virtual string FormatPropertyValue(string value)
        {
            StringBuilder sb = new StringBuilder(value);

            sb.Replace("<ProductVersion>", Application.version);
            sb.Replace("<UnityVersion>", Application.unityVersion);
            sb.Replace("<CompanyName>", Application.companyName);
            sb.Replace("<BuildGuid>", Application.buildGUID);
            sb.Replace("<ProductName>", Application.productName);
            sb.Replace("<CurrentYear>", DateTime.UtcNow.Year.ToString());

            return sb.ToString();
        }


        [Serializable]
        public class Property
        {
            [field: SerializeField] public string Name { get; set; }
            [field: SerializeField] public string Value { get; set; }

            public Property() { }

            public Property(string name, string value)
            {
                Name = name;
                Value = value;
            }
        }
    }
}

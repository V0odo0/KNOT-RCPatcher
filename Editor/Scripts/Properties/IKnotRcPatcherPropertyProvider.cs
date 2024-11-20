using System.Collections.Generic;

namespace Knot.RCPatcher.Editor
{
    public interface IKnotRcPatcherPropertyProvider
    {
        IEnumerable<KeyValuePair<string, string>> GetProperties();
    }
}

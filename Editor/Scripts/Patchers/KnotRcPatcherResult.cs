using System.Collections.Generic;

namespace Knot.RCPatcher.Editor
{
    public class KnotRcPatcherResult
    {
        public static readonly KnotRcPatcherResult Empty = new();

        public virtual List<string> FilesPatched { get; set; } = new ();
    }
}

using System.Collections.Generic;
using System.Text;

namespace Knot.RCPatcher.Editor
{
    public class KnotRcPatcherResult
    {
        public static readonly KnotRcPatcherResult Empty = new();

        public virtual List<string> FilesPatched { get; set; } = new ();

        public override string ToString()
        {
            if (FilesPatched == null || FilesPatched.Count == 0)
                return "No files patched";

            var sb = new StringBuilder();
            sb.AppendLine("Patched files:");
            foreach (var pf in FilesPatched)
                sb.AppendLine(pf);

            return sb.ToString();
        }
    }
}

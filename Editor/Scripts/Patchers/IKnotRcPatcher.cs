using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Knot.RCPatcher.Editor
{
    public interface IKnotRcPatcher
    {
        Task<KnotRcPatcherResult> Patch(IEnumerable<string> filePaths, 
            IEnumerable<KeyValuePair<string, string>> properties, 
            CancellationToken cancellationToken = default);

        IEnumerable<string> GetTargetFileExtensions();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup.Copier
{
    public record CopyData (string SourceFolder, string DestinationFolder)
    { }
    public class CopyPairs
    {
        public List<CopyData> Pairs { get; set; } = new();
    }
}

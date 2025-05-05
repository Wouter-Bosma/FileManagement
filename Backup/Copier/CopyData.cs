using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup.Copier
{
    //To link: File
    //To link: Folder

    public record CopyData(string sourceData, string DestinationFolder, bool isFolderSource)
    {
        public string ReadableString => $"{(isFolderSource ? "[FOLDER]" : "[FILE]")} [S:]{sourceData} / [T:]{DestinationFolder}";
    }
    public class CopyPairs
    {
        public List<CopyData> Pairs { get; set; } = new();
    }
}

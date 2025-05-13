using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup.Tools
{
    internal static class FileHelper
    {
        public static void Copy(string sourceFile, string destinationFile, bool overwrite)
        {
            File.Copy(sourceFile, destinationFile, overwrite);
        }

        public static async Task<long> CopyAsync(string sourceFile, string destinationFile, bool overwrite)
        {
            await using FileStream fs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            return await CopyAsync(fs, destinationFile, overwrite);
        }

        public static async Task<long> CopyAsync(Stream inStream, string outputFilePath, bool overwrite)
        {
            int bufferSize = 1024 * 1024;

            await using FileStream fileStream = new FileStream(outputFilePath, overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write);
            fileStream.SetLength(inStream.Length);
            int bytesRead = -1;
            byte[] bytes = new byte[bufferSize];

            long result = 0;
            while ((bytesRead = await inStream.ReadAsync(bytes, 0, bufferSize)) > 0)
            {
                await fileStream.WriteAsync(bytes, 0, bytesRead);
                result += bytesRead;
            }

            return result;
        }
    }
}

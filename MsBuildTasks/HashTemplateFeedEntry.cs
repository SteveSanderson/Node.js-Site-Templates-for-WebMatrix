using System;
using System.Security.Cryptography;
using Microsoft.Build.Framework;
using System.IO;

namespace WebMatrixSiteTemplates.MsBuildTasks
{
    public class HashTemplateFeedEntry : ITask
    {
        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }

        [Required] public string ZipFileName { get; set; }
        [Required] public string InputFeedEntryFile { get; set; }
        [Required] public string OutputFeedEntryFile { get; set; }

        public bool Execute() {
            // Copies the file InputFeedEntryFile to OutputFeedEntryFile
            // In the process, replaces any instance of "{templateSha1Hash}" with the SHA1 hash of the file ZipFileName
            var zipFileHash = GetSha1HashForFile(ZipFileName);
            var inputFileContents = File.ReadAllText(InputFeedEntryFile);
            var outputFileContents = inputFileContents.Replace("{templateSha1Hash}", zipFileHash);
            File.WriteAllText(OutputFeedEntryFile, outputFileContents);
            return true;
        }

        private static string GetSha1HashForFile(string filename) {
            using (var fileStream = new FileStream(filename, FileMode.Open))
            using (var sha1 = new SHA1Managed()) {
                byte[] sha1Hash = sha1.ComputeHash(fileStream);
                return BitConverter.ToString(sha1Hash).Replace("-", string.Empty).ToLower();
            }            
        }
    }
}
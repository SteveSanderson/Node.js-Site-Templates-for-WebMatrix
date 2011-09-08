using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace WebMatrixSiteTemplates.MsBuildTasks
{
    public class GetSubfolders : ITask
    {
        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }
        [Required] public string FolderPath { get; set; }
        [Output] public ITaskItem[] Results { get; set; }

        public bool Execute() {
            var subfolders = Directory.GetDirectories(FolderPath);
            Results = subfolders.Select(x => new TaskItem(Path.GetFileName(x))).ToArray();
            return true;
        }
    }
}
using ShareX.HelpersLib;
using ShareX.IndexerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    public static class IndexerHelper
    {
        public static string GetIndexFilePath(Config config, string dirPath)
        {
            string indexDir = dirPath;

            if (config.OutputMode == OutputMode.CustomDirectory && config.CustomDirectoryWithSubfolders)
                indexDir = Path.Combine(config.CustomDirectory, DateTime.Now.ToString("yyyy-MM-dd"));
            else if (config.OutputMode == OutputMode.CustomDirectory)
                indexDir = config.CustomDirectory;

            string fileName = Helpers.GetValidFileName($"{dirPath} {config.FileName}.{config.IndexerSettings.Output.ToString().ToLower()}", " ");

            if (config.PrependDate)
                fileName = $"{DateTime.Now.ToString("yyyy-MM-dd")} {fileName}";

            Helpers.CreateDirectoryIfNotExist(indexDir);

            return Path.Combine(indexDir, fileName);
        }

        public static void Index(Config config)
        {
            config.IndexerSettings.BinaryUnits = true;

            config.Folders.ForEach(dirPath =>
            {
                string index = Indexer.Index(dirPath, config.IndexerSettings);
                if (!string.IsNullOrEmpty(index))
                {
                    string filePath = GetIndexFilePath(config, dirPath);
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.Write(index);
                    }
                }
            });
        }
    }
}
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
        public static void Index(Config config)
        {
            config.IndexerSettings.BinaryUnits = true;

            config.Folders.ForEach(x =>
            {
                string index = Indexer.Index(x, config.IndexerSettings);
                if (!string.IsNullOrEmpty(index))
                {
                    string dir = config.OutputMode == OutputMode.CustomDirectory ? config.CustomDirectory : x;
                    string fileName = Helpers.GetValidFileName($"{x} {config.FileName}.{config.IndexerSettings.Output.ToString().ToLower()}", " ");
                    if (config.PrependDate)
                        fileName = $"{DateTime.Now.ToString("yyyy-MM-dd")} {fileName}";

                    if (Directory.Exists(dir))
                    {
                        string filePath = Path.Combine(dir, fileName);
                        using (StreamWriter sw = new StreamWriter(filePath))
                        {
                            sw.Write(index);
                        }
                    }
                }
            });
        }
    }
}
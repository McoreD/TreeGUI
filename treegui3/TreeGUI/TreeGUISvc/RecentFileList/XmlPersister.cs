using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WpfHelpersLib;

namespace TreeGUI
{
    internal class XmlPersister : IPersist
    {
        public string Filepath { get; set; }
        public Stream Stream { get; set; }

        public XmlPersister()
        {
            Filepath =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    WpfApplication.CompanyName + "\\" +
                    WpfApplication.ProductName + "\\" +
                    "RecentFileList.xml");
        }

        public XmlPersister(string filepath)
        {
            Filepath = filepath;
        }

        public XmlPersister(Stream stream)
        {
            Stream = stream;
        }

        public List<string> RecentFiles(int max)
        {
            return Load(max);
        }

        public void InsertFile(string filepath, int max)
        {
            Update(filepath, true, max);
        }

        public void RemoveFile(string filepath, int max)
        {
            Update(filepath, false, max);
        }

        private void Update(string filepath, bool insert, int max)
        {
            List<string> old = Load(max);

            List<string> list = new List<string>(old.Count + 1);

            if (insert) list.Add(filepath);

            CopyExcluding(old, filepath, list, max);

            Save(list, max);
        }

        private void CopyExcluding(List<string> source, string exclude, List<string> target, int max)
        {
            foreach (string s in source)
                if (!String.IsNullOrEmpty(s))
                    if (!s.Equals(exclude, StringComparison.OrdinalIgnoreCase))
                        if (target.Count < max)
                            target.Add(s);
        }

        private SmartStream OpenStream(FileMode mode)
        {
            if (!String.IsNullOrEmpty(Filepath))
            {
                return new SmartStream(Filepath, mode);
            }
            else
            {
                return new SmartStream(Stream);
            }
        }

        private List<string> Load(int max)
        {
            List<string> list = new List<string>(max);

            using (MemoryStream ms = new MemoryStream())
            {
                using (SmartStream ss = OpenStream(FileMode.OpenOrCreate))
                {
                    if (ss.Stream.Length == 0) return list;

                    ss.Stream.Position = 0;

                    byte[] buffer = new byte[1 << 20];
                    for (;;)
                    {
                        int bytes = ss.Stream.Read(buffer, 0, buffer.Length);
                        if (bytes == 0) break;
                        ms.Write(buffer, 0, bytes);
                    }

                    ms.Position = 0;
                }

                XmlTextReader x = null;

                try
                {
                    x = new XmlTextReader(ms);

                    while (x.Read())
                    {
                        switch (x.NodeType)
                        {
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.Whitespace:
                                break;

                            case XmlNodeType.Element:
                                switch (x.Name)
                                {
                                    case "RecentFiles": break;

                                    case "RecentFile":
                                        if (list.Count < max) list.Add(x.GetAttribute(0));
                                        break;

                                    default: Debug.Assert(false); break;
                                }
                                break;

                            case XmlNodeType.EndElement:
                                switch (x.Name)
                                {
                                    case "RecentFiles": return list;
                                    default: Debug.Assert(false); break;
                                }
                                break;

                            default:
                                Debug.Assert(false);
                                break;
                        }
                    }
                }
                finally
                {
                    if (x != null) x.Close();
                }
            }
            return list;
        }

        private void Save(List<string> list, int max)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                XmlTextWriter x = null;

                try
                {
                    x = new XmlTextWriter(ms, Encoding.UTF8);
                    if (x == null) { Debug.Assert(false); return; }

                    x.Formatting = Formatting.Indented;

                    x.WriteStartDocument();

                    x.WriteStartElement("RecentFiles");

                    foreach (string filepath in list)
                    {
                        x.WriteStartElement("RecentFile");
                        x.WriteAttributeString("Filepath", filepath);
                        x.WriteEndElement();
                    }

                    x.WriteEndElement();

                    x.WriteEndDocument();

                    x.Flush();

                    using (SmartStream ss = OpenStream(FileMode.Create))
                    {
                        ss.Stream.SetLength(0);

                        ms.Position = 0;

                        byte[] buffer = new byte[1 << 20];
                        for (;;)
                        {
                            int bytes = ms.Read(buffer, 0, buffer.Length);
                            if (bytes == 0) break;
                            ss.Stream.Write(buffer, 0, bytes);
                        }
                    }
                }
                finally
                {
                    if (x != null) x.Close();
                }
            }
        }
    }
}
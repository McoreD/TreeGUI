﻿#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (c) 2007-2016 ShareX Team

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using HelpersLib;
using System;
using System.IO;
using System.Text;

namespace ShareX.IndexerLib
{
    public class IndexerText : Indexer
    {
        protected StringBuilder sbContent = new StringBuilder();

        public IndexerText(IndexerSettings indexerSettings) : base(indexerSettings)
        {
        }

        public override string Index(string folderPath)
        {
            StringBuilder sbTxtIndex = new StringBuilder();

            FolderInfo folderInfo = GetFolderInfo(folderPath);
            folderInfo.Update();

            IndexFolder(folderInfo);
            string index = sbContent.ToString().Trim();

            sbTxtIndex.AppendLine(index);
            if (settings.AddFooter)
            {
                string footer = GetFooter();
                sbTxtIndex.AppendLine("_".Repeat(footer.Length));
                sbTxtIndex.AppendLine(footer);
            }
            return sbTxtIndex.ToString().Trim();
        }

        protected override void IndexFolder(FolderInfo dir, int level = 0)
        {
            sbContent.AppendLine(GetFolderNameRow(dir, level));

            foreach (FolderInfo subdir in dir.Folders)
            {
                if (settings.AddEmptyLineAfterFolders)
                {
                    sbContent.AppendLine();
                }

                IndexFolder(subdir, level + 1);
            }

            if (dir.Files.Count > 0)
            {
                if (settings.AddEmptyLineAfterFolders)
                {
                    sbContent.AppendLine();
                }

                foreach (FileInfo fi in dir.Files)
                {
                    sbContent.AppendLine(GetFileNameRow(fi, level + 1));
                }
            }
        }

        private string GetFolderNameRow(FolderInfo dir, int level)
        {
            string folderNameRow = string.Format("{0}{1}", settings.IndentationText.Repeat(level), dir.FolderName);

            if (settings.ShowSizeInfo && dir.Size > 0)
            {
                folderNameRow += string.Format(" [{0}]", dir.Size.ToSizeString(settings.BinaryUnits));
            }

            return folderNameRow;
        }

        private string GetFileNameRow(FileInfo fi, int level)
        {
            string fileNameRow = settings.IndentationText.Repeat(level) + fi.Name;

            if (settings.ShowSizeInfo)
            {
                fileNameRow += string.Format(" [{0}]", fi.Length.ToSizeString(settings.BinaryUnits));
            }

            return fileNameRow;
        }

        private string GetFooter()
        {
            return $"Generated by ShareX Directory Indexer on {DateTime.UtcNow.ToString("yyyy-MM-dd 'at' HH:mm:ss 'UTC'")}. Latest version can be downloaded from: https://getsharex.com/";
        }
    }
}
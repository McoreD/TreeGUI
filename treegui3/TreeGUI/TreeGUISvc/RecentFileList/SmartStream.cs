using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGUI
{
    internal class SmartStream : IDisposable
    {
        private bool _IsStreamOwned = true;
        private Stream _Stream = null;

        public Stream Stream { get { return _Stream; } }

        public static implicit operator Stream(SmartStream me)
        {
            return me.Stream;
        }

        public SmartStream(string filepath, FileMode mode)
        {
            _IsStreamOwned = true;

            Directory.CreateDirectory(Path.GetDirectoryName(filepath));

            _Stream = File.Open(filepath, mode);
        }

        public SmartStream(Stream stream)
        {
            _IsStreamOwned = false;
            _Stream = stream;
        }

        public void Dispose()
        {
            if (_IsStreamOwned && _Stream != null) _Stream.Dispose();

            _Stream = null;
        }
    }
}
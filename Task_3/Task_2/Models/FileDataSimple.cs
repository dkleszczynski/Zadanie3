using System;
using System.Collections.Generic;
using System.Text;

namespace Task_2.Models
{
    public class FileDataSimple
    {
        public string FullName { get; private set; }

        public DateTime CreationTime { get; private set;}

        public DateTime LastAccessTime { get; private set; }

        public DateTime LastWriteTime { get; private set; }

        public bool IsHidden { get; private set; }

        public bool IsReadOnly { get; private set; }

        public FileDataSimple(FileData fd)
        {
            FullName = fd.FileInfo.FullName;
            CreationTime = fd.FileInfo.CreationTime;
            LastAccessTime = fd.FileInfo.LastAccessTime;
            LastWriteTime = fd.FileInfo.LastWriteTime;
            IsHidden = fd.IsHidden;
            IsReadOnly = fd.IsReadOnly;

        }
    }
}

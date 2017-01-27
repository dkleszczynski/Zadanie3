using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.AccessControl;
using System.Collections;

namespace Task_2.Models
{
    /// <summary>
    /// Model representation of file metadata.
    /// </summary>
    public class FileData : IFileData
    {
        public FileInfo FileInfo { get; private set; }
             
        /// <summary>
        /// AccessControlSections.All: Entire Security Descriptor
        /// </summary>
        public AuthorizationRuleCollection AccessRules { get; private set; }
        
        private FileAttributes fileAttributes;


        public FileData(FileInfo fileInfo, FileAttributes fileAttributes)
        {
            FileInfo = fileInfo;
            this.fileAttributes = fileAttributes;
            AccessRules = null;
         }

        public FileData(FileInfo fileInfo, FileAttributes fileAttributes, AuthorizationRuleCollection accessRules)
        {
            FileInfo = fileInfo;
            this.fileAttributes = fileAttributes;
            AccessRules = accessRules;
        }

        public bool IsArchive
        {
            get
            {
                if ((fileAttributes & FileAttributes.Archive) == FileAttributes.Archive)
                    return true;
                else
                    return false;
            }
        }

        public bool IsCompressed
        {
            get
            {
                if ((fileAttributes & FileAttributes.Compressed) == FileAttributes.Compressed)
                    return true;
                else
                    return false;
            }
        }

        public bool IsEncrypted
        {
            get
            {
                if ((fileAttributes & FileAttributes.Encrypted) == FileAttributes.Encrypted)
                    return true;
                else
                    return false;
            }
        }

        public bool IsHidden
        {
            get
            {
                if ((fileAttributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    return true;
                else
                    return false;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    return true;
                else
                    return false;
            }
        }

        public bool IsSystemFile
        {
            get
            {
                if ((fileAttributes & FileAttributes.System) == FileAttributes.System)
                    return true;
                else
                    return false;
            }
        }

        public bool IsTemporary
        {
            get
            {
                if ((fileAttributes & FileAttributes.Temporary) == FileAttributes.Temporary)
                    return true;
                else
                    return false;
            }
        }

      
    }
}


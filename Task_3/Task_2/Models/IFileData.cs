using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace Task_2.Models
{
     public interface IFileData
    {
        FileInfo FileInfo { get; }

        AuthorizationRuleCollection AccessRules { get; }

        
    }
}

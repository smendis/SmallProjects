using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoreDatabaseBackup
{
    public interface IAccessController
    {
        void PermitFileSystemAccess(string targetLocation);
    }
}

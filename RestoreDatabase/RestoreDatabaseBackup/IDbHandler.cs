using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;

namespace RestoreDatabaseBackup
{
    public interface IDbHandler
    {
        void BackupDatabase();
        void RestoreDatabase();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RestoreDatabaseBackup
{
    public class AccessController : IAccessController
    {
        public void PermitFileSystemAccess(string targetLocation)
        {
            try
            {
                DirectorySecurity dirSec = Directory.GetAccessControl(targetLocation);
                FileSystemAccessRule fsar = new FileSystemAccessRule
                (@"NT AUTHORITY\NETWORK SERVICE"
                , FileSystemRights.FullControl
                , InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit
                , PropagationFlags.None
                , AccessControlType.Allow);
                dirSec.AddAccessRule(fsar);
                Directory.SetAccessControl(targetLocation, dirSec);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoreDatabaseBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            IAccessController accessController = new AccessController();
            IDbHandler db = new DbHandler();

            while (true)
            {
                Console.WriteLine("Please select the action to perform. B - Backup, R - Restore, Q - quit");
                string input = Console.ReadLine().ToUpperInvariant();
                switch (input)
                {
                    case "B":
                        db.BackupDatabase();
                        break;
                    case "R":
                        db.RestoreDatabase();
                        break;
                    case "Q":
                        return;
                    default:
                        Console.WriteLine("Command not identified...");
                        break;
                }
                Console.WriteLine();
            }
            
        }
    }

}

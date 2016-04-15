using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace RestoreDatabaseBackup
{
    public class DbHandler : IDbHandler
    {
        private string databaseName;
        private string serverName;
        private string username;
        private string password;
        private string backupFile;

        private ServerConnection connection;
        private Server server;
        
        private Logger logger;

        public DbHandler()
        {
            databaseName = Settings.DatabaseName;
            serverName = Settings.ServerName;
            username = Settings.Username;
            password = Settings.Password;
            backupFile = Settings.BackupFile;

            if (string.IsNullOrEmpty(username))
            {
                string connectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=true;",
                    serverName, databaseName);
                connection = new ServerConnection(connectionString);
            }
            else
            {
                connection = new ServerConnection(serverName, username, password);
            }

            server = new Server(connection);

            logger = Logger.CreateLoggerInstance();
        }

        public void RestoreDatabase()
        {
            connection.Connect();

            Restore restore = new Restore();
            restore.Action = RestoreActionType.Database;
            restore.Database = databaseName;
            restore.ReplaceDatabase = true;
            restore.Devices.AddDevice(backupFile, DeviceType.File);

            restore.PercentCompleteNotification = 100;
            restore.PercentComplete += RestoreOnPercentComplete;

            server.KillAllProcesses(databaseName);

            logger.Log("Restoring database '"+databaseName+"'! Please wait...");

            try
            {
                restore.SqlRestore(server);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            connection.Disconnect();
        }

        private void RestoreOnPercentComplete(object sender, PercentCompleteEventArgs percentCompleteEventArgs)
        {
            logger.Log("Restoration completed "+percentCompleteEventArgs.Percent+"%");
        }

        public void BackupDatabase()
        {
            connection.Connect();

            Backup backup = new Backup();
            backup.Action = BackupActionType.Database;
            backup.Database = databaseName;
            backup.BackupSetName = databaseName;
            backup.Devices.AddDevice(backupFile, DeviceType.File);

            backup.PercentCompleteNotification = 100;
            backup.PercentComplete += BackupOnPercentComplete;

            server.KillAllProcesses(databaseName);

            logger.Log("Backup executing on database '" + databaseName + "'! Please wait...");

            try
            {
                backup.SqlBackup(server);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            connection.Disconnect();
        }

        private void BackupOnPercentComplete(object sender, PercentCompleteEventArgs percentCompleteEventArgs)
        {
            logger.Log("Backup action completed " + percentCompleteEventArgs.Percent + "%");
            logger.Log("Backup stored at " + backupFile);
        }
    }
}

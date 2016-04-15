using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoreDatabaseBackup
{
    public class Settings
    {
        private static string _databaseName = string.Empty;
        public static string DatabaseName
        {
            get
            {
                if (string.IsNullOrEmpty(_databaseName))
                    _databaseName =ConfigurationManager.AppSettings["DatabaseName"];

                return _databaseName;
            }
        }

        private static string _serverName = string.Empty;
        public static string ServerName
        {
            get
            {
                if (string.IsNullOrEmpty(_serverName))
                    _serverName = ConfigurationManager.AppSettings["ServerName"];

                return _serverName;
            }
        }

        private static string _username = string.Empty;
        public static string Username
        {
            get
            {
                if (string.IsNullOrEmpty(_username))
                    _username = ConfigurationManager.AppSettings["Username"];

                return _username;
            }
        }

        private static string _password = string.Empty;
        public static string Password
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                    _password = ConfigurationManager.AppSettings["Password"];

                return _password;
            }
        }

        private static string _backupFile = string.Empty;
        public static string BackupFile
        {
            get
            {
                if (string.IsNullOrEmpty(_backupFile))
                    _backupFile = ConfigurationManager.AppSettings["BackupFileFullPathName"];

                return _backupFile;
            }
        }
    }
}

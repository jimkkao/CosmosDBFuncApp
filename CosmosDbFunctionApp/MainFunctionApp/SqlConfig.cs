using System;
using System.Collections.Generic;
using System.Text;

using RepositoryContract;

namespace MainFunctionApp
{
    public class SqlConfig : ISqlConfig
    {
        public SqlConfig()
        {
            EndPointUrl = GetEndPointUrl();
            PrimaryKey = GetPrimaryKey();

        }
        public SqlConfig(string endPointUrl, string primaryKey, string database, string container, string partitionKey)
        {
            EndPointUrl = endPointUrl;
            PrimaryKey = primaryKey;
            DataBase = database;
            Container = container;
            PartitionKey = partitionKey;
        }

        public static string GetEndPointUrl()
        {
            var val = System.Environment.GetEnvironmentVariable("EndPointUrl");

            val = string.IsNullOrEmpty(val) ? string.Empty : val;

            return val;
        }

        public static string GetPrimaryKey()
        {
            var val = System.Environment.GetEnvironmentVariable("PrimaryKey");

            val = string.IsNullOrEmpty(val) ? string.Empty : val;

            return val;
        }

        public static string GetDatabase()
        {
            var val = System.Environment.GetEnvironmentVariable("Database");

            val = string.IsNullOrEmpty(val) ? string.Empty : val;

            return val;
        }

        public static string GetContainer()
        {
            var val = System.Environment.GetEnvironmentVariable("Container");

            val = string.IsNullOrEmpty(val) ? string.Empty : val;

            return val;
        }

        public string EndPointUrl
        {
            get;
            set;
        }
        

        public string PrimaryKey
        {
            get;
            set;
        } 
        public string DataBase
        {
            get;
            set;
        }
        
        public string Container
        {
            get;
            set;
        }

        public string PartitionKey
        {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using RepositoryContract;

namespace MainFunctionApp
{
    public class SqlConfig : ISqlConfig
    {
        public SqlConfig(string endPointUrl, string primaryKey, string database, string container, string partitionKey)
        {
            EndPointUrl = endPointUrl;
            PrimaryKey = primaryKey;

            DataBase = database;

            Container = container;

            PartitionKey = partitionKey;
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

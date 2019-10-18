using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

using RepositoryContract;

namespace MainFunctionApp
{
    public class MongoConfig : IMongoConfig
    {
        public MongoConfig()
        {
            ConnectionStr = GetMongoAPIConnectionStr();

            Database = GetMongoAPIDatabase();

            Collection = GetMongoAPICollection();
        }

        public MongoConfig(string connStr, string database, string collection)
        {
            ConnectionStr = connStr;
            Database = database;
            Collection = collection;
        }


        public static string GetMongoAPIConnectionStr()
        {
            var val = System.Environment.GetEnvironmentVariable("MongoConnectionStr");

            val = string.IsNullOrEmpty(val) ? string.Empty : val;

            return val;
        }


        public static string GetMongoAPIDatabase()
        {
            var val = System.Environment.GetEnvironmentVariable("MongoDatabase");

            return val;
        }

        public static string GetMongoAPICollection()
        {
            var val = System.Environment.GetEnvironmentVariable("MongoCollection");

            return val;
        }


        public string ConnectionStr 
        {
            get;
            set;
        }

        public string Database
        {
            get;
            set;
        }
     
        public string Collection 
        {
            get;
            set;
        }
    }
}

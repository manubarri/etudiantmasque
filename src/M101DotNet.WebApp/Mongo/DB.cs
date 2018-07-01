using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using M101DotNet.WebApp.Models.Account;

namespace EtudiantMasque.Models
{
    public class DB
    {
        public const string CONNECTION_STRING_NAME = "EtudiantMasque";
        public const string DATABASE_NAME = "etudiantmasque";
        public const string ARTICLES_COLLECTION_NAME = "articles";
        public const string USERS_COLLECTION_NAME = "users";
        public const string CONTACT_COLLECTION_NAME = "messages";

        private static DB Instance;
        private static IMongoClient _client;
        private static IMongoDatabase _database;

        private DB()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(DATABASE_NAME);
        }

        public static DB getInstance()
        {
            Instance = Instance??new DB();
            return Instance;
        }

        public IMongoClient Client
        {
            get { return _client; }
        }

        public IMongoCollection<Article> Articles
        {
            get { return _database.GetCollection<Article>(ARTICLES_COLLECTION_NAME); }
        }

        public IMongoCollection<Register> Users
        {
            get { return _database.GetCollection<Register>(USERS_COLLECTION_NAME); }
        }

        public IMongoCollection<Message> Messages
        {
            get { return _database.GetCollection<Message>(CONTACT_COLLECTION_NAME); }
        }
    }
}
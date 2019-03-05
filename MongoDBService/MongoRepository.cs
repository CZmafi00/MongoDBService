using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBService
{
    public class MongoRepository
    {
        #region Fields
        private MongoDatabase database;
        #endregion


        #region Constructors
        public MongoRepository(MongoDatabase db)
        {
            this.database = db;
        }
        #endregion


        #region Public methods
        public List<TDocument> GetAllDocuments<TDocument>(string collectionName) where TDocument : BaseDocument
        {
            var collection = GetCollection<TDocument>(collectionName);

            return collection.Find(FilterDefinition<TDocument>.Empty).ToCursor().ToList();
        }
        public List<TDocument> GetDocuments<TDocument>(string collectionName, FilterDefinition<TDocument> filter) where TDocument : BaseDocument
        {
            var collection = GetCollection<TDocument>(collectionName);

            return collection.Find(filter).ToCursor().ToList();
        }
        public List<TDocument> GetDocuments<TDocument>(string collectionName, Expression<Func<TDocument, bool>> filter) where TDocument : BaseDocument
        {
            var collection = GetCollection<TDocument>(collectionName);

            return collection.Find(filter).ToCursor().ToList();
        }

        public bool ReplaceDocument<TDocument>(TDocument document) where TDocument : BaseDocument
        {
            var collection = GetCollection<TDocument>(document);

            var filter = Builders<TDocument>.Filter.Eq(d => d.Id, document.Id);
            var result = collection.ReplaceOne(filter, document);

            if (!result.IsAcknowledged)
                throw new Exception("Document was not updated.");

            return result.ModifiedCount > 0;
        }
        public void InsertDocument<TDocument>(TDocument document) where TDocument : BaseDocument
        {
            var collection = GetCollection<TDocument>(document);

            collection.InsertOne(document);
        }
        public bool DeleteDocument<TDocument>(TDocument document) where TDocument : BaseDocument
        {
            var collection = GetCollection<TDocument>(document);

            var filter = Builders<TDocument>.Filter.Eq(d => d.Id, document.Id);
            var result = collection.DeleteOne(filter);

            if (!result.IsAcknowledged)
                throw new Exception("Document was not updated.");

            return result.DeletedCount > 0;
        }
        #endregion


        #region Private methods
        private IMongoDatabase GetDatabase()
        {
            MongoClient client = new MongoClient(database.ConnectionString);
            return client.GetDatabase(database.Name);
        }
        private IMongoCollection<TDocument> GetCollection<TDocument>(TDocument document) where TDocument : BaseDocument
        {
            var db = GetDatabase();

            if (document.Collection.TimeOriented)
                return db.GetCollection<TDocument>(document.Collection.GetTimeOrientedName());
            else
                return db.GetCollection<TDocument>(document.Collection.Name);
        }
        private IMongoCollection<TDocument> GetCollection<TDocument>(string collection) where TDocument : BaseDocument
        {
            var db = GetDatabase();
            return db.GetCollection<TDocument>(collection);
        }
        #endregion
    }
}

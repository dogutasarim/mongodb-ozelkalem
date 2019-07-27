using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;

namespace ozelkalem
{
    class mongo
    {
        MongoClient client;
        public IMongoDatabase db;
        string dbName = "randevular";
        string collectionName;
        public mongo(string colName)
        {
            client = new MongoClient();
            db = client.GetDatabase(dbName);
            collectionName = colName;
        }

       
        public bool insert<T>(dynamic doc)
        {
            bool result = false;
            try
            {
                IMongoCollection<T> collection = db.GetCollection<T>(collectionName);
                collection.InsertOne(doc);
                result = true;
            }
            catch { }
            return result;
        }
        public bool update<T>(Expression<Func<T, bool>> find, UpdateDefinition<T> newdoc)
        {
            bool result = false;
            try
            {
                IMongoCollection<T> collection = db.GetCollection<T>(collectionName);
                collection.UpdateOne<T>(find, newdoc);
                result = true;
            }
            catch { }
            return result;
        }
        public bool delete<T>(Expression<Func<T, bool>> find)
        {
            bool result = false;
            try
            {
                IMongoCollection<T> collection = db.GetCollection<T>(collectionName);
                collection.DeleteOne<T>(find);
                result = true;
            }
            catch { }
            return result;
        }
        public List<T> findAll<T>(FilterDefinition<T> filter = null)
        {
            dynamic result = null;
            try
            {
                if (filter == null)
                {
                    result = db.GetCollection<T>(collectionName).Find<T>(_ => true).ToList<T>();
                }
                else
                {
                    result = db.GetCollection<T>(collectionName).Find<T>(filter).ToList<T>();
                }

            }
            catch { }

            return result;
        }
    }
}

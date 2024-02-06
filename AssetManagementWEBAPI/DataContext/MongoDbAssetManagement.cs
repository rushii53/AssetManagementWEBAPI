using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.DataContext
{
    public class MongoDbAssetManagement
    {
        public readonly IMongoCollection<Machine> _machineCollection;
        public MongoDbAssetManagement(
            IOptions<DBModel> mongodConnection)
        {
            var mongoClient = new MongoClient(
                mongodConnection.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongodConnection.Value.DatabaseName);

            _machineCollection = mongoDatabase.GetCollection<Machine>(
                mongodConnection.Value.CollectionName);
        }
    }
}

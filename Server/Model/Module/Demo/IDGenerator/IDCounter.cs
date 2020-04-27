using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [BsonIgnoreExtraElements]
    public class IDCounter : ComponentWithId
    {
        public int sequence_value { get; set; }

        public static int getNextId()
        {
            IMongoDatabase mongoDatabase = Game.Scene.GetComponent<DBComponent>().mongoClient.GetDatabase("ET");
            IMongoCollection<IDCounter> collection = mongoDatabase.GetCollection<IDCounter>("counters");
            var filter = Builders<IDCounter>.Filter;
            var update = Builders<IDCounter>.Update;

            IDCounter result = collection.FindOneAndUpdate(filter.Eq("_id", 1), update.Inc("sequence_value", 1));
            if (result != null)
            {
                return result.sequence_value;
            }
            return 0;
        }
    }
}

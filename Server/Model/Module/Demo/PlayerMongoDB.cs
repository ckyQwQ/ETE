using MongoDB.Bson.Serialization.Attributes;

namespace ETModel
{
    public class PlayerMongoDB : ComponentWithId
    {
        public PlayerMongoDB() : base(IDCounter.getNextId())
        {
        }

        public string name { get; set; }
        public string password { get; set; }
        [BsonDefaultValue(0L)]
        public int sex { get; set; }
    }
}

using ETModel;
using MongoDB.Driver;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_RegisterHandler : AMRpcHandler<C2R_Register, R2C_Register>
    {
        protected override async ETTask Run(Session session, C2R_Register request, R2C_Register response, Action reply)
        {
            string account = request.Account;
            string password = request.Password;

            if (CheckAccountIfExist(account))
            {
                response.Error = ErrorCode.ERR_AccountAlreadyExist;
                response.RegisterStatus = false;
                reply();
                return;
            }

            IMongoDatabase db = Game.Scene.GetComponent<DBComponent>().mongoClient.GetDatabase("ET");
            IMongoCollection<PlayerMongoDB> collection = db.GetCollection<PlayerMongoDB>("player");

            PlayerMongoDB player = new PlayerMongoDB() { name = account, password = password };
            await collection.InsertOneAsync(player);
            response.RegisterStatus = true;
            reply();
        }

        public bool CheckAccountIfExist(string account)
        {
            if (account == null)
            {
                return false;
            }
            IMongoDatabase db = Game.Scene.GetComponent<DBComponent>().mongoClient.GetDatabase("ET");
            IMongoCollection<PlayerMongoDB> collection = db.GetCollection<PlayerMongoDB>("player");

            var filter = Builders<PlayerMongoDB>.Filter;
            var result = collection.FindSync(filter.Eq("name", account));
            result.MoveNext();

            foreach (PlayerMongoDB t1 in result.Current)
            {
                return true;
            }

            return false;
        }
    }
}
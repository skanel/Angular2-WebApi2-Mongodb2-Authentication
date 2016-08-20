namespace AngularJSAuthentication.API
{
    using Entities;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;
    using System.Configuration;

    public class MongoContext : IMongoContext
    {
        private readonly IMongoCollection<User> userCollection;
        private readonly IMongoCollection<Role> roleCollection;
        private readonly IMongoCollection<Client> clientCollection;
        private readonly IMongoCollection<RefreshToken> refreshTokenCollection;

        public MongoContext()
        {
            var pack = new ConventionPack()
            {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("CamelCaseConvensions", pack, t => true);

            var mongoUrlBuilder = new MongoUrlBuilder(ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString);

            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
           // var server = mongoClient.GetServer();

            Database = mongoClient.GetDatabase(mongoUrlBuilder.DatabaseName);

            userCollection = Database.GetCollection<User>("users");
            roleCollection = Database.GetCollection<Role>("roles");
            clientCollection = Database.GetCollection<Client>("clients");
            refreshTokenCollection = Database.GetCollection<RefreshToken>("refreshTokens");
        }

        public IMongoDatabase Database { get; private set; }

        public IMongoCollection<User> Users
        {
            get { return userCollection; }
        }

        public IMongoCollection<Role> Roles
        {
            get { return roleCollection; }
        }

        public IMongoCollection<Client> Clients
        {
            get { return clientCollection; }
        }

        public IMongoCollection<RefreshToken> RefreshTokens
        {
            get { return refreshTokenCollection; }
        }
    }
}

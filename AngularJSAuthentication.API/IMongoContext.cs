namespace AngularJSAuthentication.API
{
    using Entities;
    using MongoDB.Driver;

    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
        IMongoCollection<User> Users { get; }
        IMongoCollection<Role> Roles { get; }
        IMongoCollection<Client> Clients { get; }
        IMongoCollection<RefreshToken> RefreshTokens { get; }
    }
}
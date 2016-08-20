namespace AngularJSAuthentication.API
{
    using Entities;
    using Models;
    using Microsoft.AspNet.Identity;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AuthRepository
    {
        private readonly IMongoContext mongoContext;
        private readonly ApplicationUserManager userManager;

        public AuthRepository(IMongoContext mongoContext, ApplicationUserManager userManager)
        {
            this.mongoContext = mongoContext;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new User
            {
                UserName = userModel.UserName
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await userManager.FindAsync(userName, password);

            return user;
        }

        public Client FindClient(string clientId)
        {
            var query = Builders<Client>.Filter.Where(c => c.Id == clientId);
            var client = mongoContext.Clients.Find(query).Limit(1).FirstOrDefault();

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

          
            var query =  Builders<RefreshToken>.Filter.Where(r => r.Subject==token.Subject) & Builders<RefreshToken>.Filter.Where(r => r.ClientId==token.ClientId);
            var existingToken = mongoContext.RefreshTokens.Find(query).Limit(1).SingleOrDefault();
            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            mongoContext.RefreshTokens.InsertOneAsync(token);

            return true;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {

            var query = Builders< RefreshToken >.Filter.Where(r => r.Id==refreshTokenId);
            var writeConcernResult = await mongoContext.RefreshTokens.DeleteOneAsync(query);
            return await Task.FromResult(writeConcernResult.DeletedCount == 1);
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            return await RemoveRefreshToken(refreshToken.Id);
        }

        public Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var query = Builders<RefreshToken>.Filter.Where(r => r.Id== refreshTokenId);

            var refreshToken = mongoContext.RefreshTokens.Find(query).Limit(1).FirstOrDefault();

            return Task.FromResult(refreshToken);
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return mongoContext.RefreshTokens.Find(new BsonDocument()).ToList();
        }
    }
}
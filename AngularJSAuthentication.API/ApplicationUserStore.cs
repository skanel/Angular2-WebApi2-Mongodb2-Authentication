namespace AngularJSAuthentication.API
{
    using AspNet.Identity.MongoDB;
    using Entities;
    using MongoDB.Driver;

    public class ApplicationUserStore : UserStore<User>
    {
        IMongoCollection<User> collection;
        public ApplicationUserStore(IMongoContext context) : base(context.Users)
        {
            collection = context.Users;
        }
    }
}
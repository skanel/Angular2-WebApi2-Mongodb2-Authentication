namespace AngularJSAuthentication.API
{
    using Entities;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MongoDB.Driver;
    public class ApplicationIdentityContext : IdentityContext
    {
        public ApplicationIdentityContext(IMongoContext mongoContext)
            : this(mongoContext.Users, mongoContext.Roles)
        {
        }
            
        public ApplicationIdentityContext(IMongoCollection<User> users, IMongoCollection<Role> roles) : base(users, roles)
       {
       }
         
    }
}
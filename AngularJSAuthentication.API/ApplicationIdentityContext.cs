namespace AngularJSAuthentication.API
{
    using Entities;
    using MongoDB.Driver;
    public class ApplicationIdentityContext
    {
        private IMongoContext mongoContext;
        private ApplicationUserManager userManager;

        public ApplicationIdentityContext(
            IMongoContext mongoContext,
            ApplicationUserManager userManager)           
        {
            this.mongoContext = mongoContext;
            this.userManager = userManager;
        }                
    }
}
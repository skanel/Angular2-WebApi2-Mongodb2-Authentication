namespace AngularJSAuthentication.API
{
    using AspNet.Identity.MongoDB;
    using Entities;
    using Microsoft.AspNet.Identity;

    public class ApplicationRoleManager : RoleManager<Role>
    {
        private IMongoContext mongoContext;

        public ApplicationRoleManager(IMongoContext mongoContext)
            : base(new RoleStore<Role>(mongoContext.Roles))
        {
            this.mongoContext = mongoContext;
        }
    }
}
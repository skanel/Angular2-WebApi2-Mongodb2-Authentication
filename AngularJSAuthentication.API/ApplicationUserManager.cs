namespace AngularJSAuthentication.API
{
    using Entities;
    using Microsoft.AspNet.Identity;

    public class ApplicationUserManager : UserManager<User>
    {
        private IUserStore<User> accountStore;

        public ApplicationUserManager(IUserStore<User> userStore)
            : base(userStore)
        {
            this.accountStore = userStore;
        }
    }
}
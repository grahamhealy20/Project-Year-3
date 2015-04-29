using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace WebApplication1.Helpers
{
    public class UserHelper
    {
        // Need a dedicated context for users manager to be thread safe.
        private static UserStore<Models.ApplicationUser> store = new UserStore<Models.ApplicationUser>(new Models.UserDbContext());
        private static UserManager<Models.ApplicationUser> manager = new UserManager<Models.ApplicationUser>(store);
        static public Models.ApplicationUser getUser(string userId)
        {
            try
            {
                var user = manager.FindById(userId);
                return user;
            }
            catch (Exception)
            {              
                throw;
            }
          
        }

        static public void updateUser(Models.ApplicationUser user) {
            manager.UpdateAsync(user);
            var ctx = store.Context;
            ctx.SaveChanges();
        }
    }
}
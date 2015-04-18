﻿using System;
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
        private static UserStore<Models.ApplicationUser> store = new UserStore<Models.ApplicationUser>(new Models.ApplicationDbContext());
        private static UserManager<Models.ApplicationUser> manager = new UserManager<Models.ApplicationUser>(store);
        static public Models.ApplicationUser getUser(string userId)
        {

            var user = manager.FindById(userId);
            return user;
        }

        static public void updateUser(Models.ApplicationUser user) {
            manager.UpdateAsync(user);
            var ctx = store.Context;
            ctx.SaveChanges();
        }
    }
}
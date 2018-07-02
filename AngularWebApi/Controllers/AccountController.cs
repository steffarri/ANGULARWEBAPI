using AngularWebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using System.Security.Cryptography;

namespace AngularWebApi.Controllers
{

    public class AccountController : ApiController
    {
        private ApplicationDbContext _application;



        [Route("api/User/Register")]
        [HttpPost]
        public IdentityResult Register(AccountModel model)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3
            };
            IdentityResult result = manager.Create(user, model.Password);
            return result;
        }

        [Route("api/User/GetUser")]
        [HttpPost]
        public ApplicationUser GetUser(AccountModel model)
        {
            var userRoles = new List<ApplicationUser>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var newdb = new ApplicationDbContext();
            try
            {
                foreach (var i in userStore.Users)
                {
                    userRoles.Add(i);
                }

            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }


            var p = new PasswordHasher();

            var getCredentials = userRoles.FirstOrDefault<ApplicationUser>(a => a.UserName == model.UserName);
            var getPassword = getCredentials.PasswordHash;

            PasswordVerificationResult verification = p.VerifyHashedPassword(getPassword, model.Password);
           
           
            if (getCredentials != null && verification != PasswordVerificationResult.Failed)
                return getCredentials;
            else
                return null;
        }

      
    }
}

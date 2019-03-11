using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ServerRequestWebApp.Models
{
    public class AdAuthenticationService
    {
        public string authUser;
        private string[] adMembergroups = null;
        public bool admin = false;
        public class AuthenticationResult
        {
            public AuthenticationResult(string errorMessage = null)
            {
                ErrorMessage = errorMessage;
            }

            public string ErrorMessage { get; private set; }
            public Boolean IsSuccess => string.IsNullOrEmpty(ErrorMessage);
        }



        private readonly IAuthenticationManager authenticationManager;

        public AdAuthenticationService(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;

        }

        public AuthenticationResult SignIn(String username, String password)
        {
#if DEBUG
            // authenticates against your local machine - for development time
            ContextType authenticationType = ContextType.Domain;
#else

            // authenticates against your Domain AD
            ContextType authenticationType = ContextType.Machine;
#endif
            try
            {
                PrincipalContext principalContext = new PrincipalContext(authenticationType, "CRAFTSILICON.LOCAL");
           
          
            bool isAuthenticated = false;
            UserPrincipal userPrincipal = null;
            try
            {
                userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                if (userPrincipal != null)
                {
                    isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);
                }
            }
            catch (Exception exception)
            {

                return new AuthenticationResult("Username or Password is not correct");
            }

            if (!isAuthenticated)
            {
                return new AuthenticationResult("Username or Password is not correct");
            }

            if (userPrincipal.IsAccountLockedOut())
            {
                return new AuthenticationResult("Your account is locked.");
            }

            if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
            {
                return new AuthenticationResult("Your account is disabled");
            }

            var identity = CreateIdentity(userPrincipal);
            GetGroups(principalContext,userPrincipal);

            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

            MySession.Current.userName = authUser;
            MySession.Current.IsAdmin = admin;
            MySession.Current.MyDate = DateTime.Now;
            return new AuthenticationResult();

            }
            catch (Exception e)
            {
                return new AuthenticationResult(e.Message);
            }
        }

        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(MyAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
            authUser = userPrincipal.SamAccountName;

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
            if (!String.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }
            var groups = userPrincipal.GetAuthorizationGroups();
            foreach (var @group in groups)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, @group.Name));
            }
            return identity;
        }

        private void GetGroups(PrincipalContext principalContext, UserPrincipal user)
        {
            using (principalContext)
            {
                using (user)
                {
                    if (user != null)
                    {
                        adMembergroups = user.GetGroups()
                            .Select(x => x.SamAccountName)
                            .ToArray();
                    }
                }
            }
            IsAdmin(adMembergroups);
        }

        private void IsAdmin(string[] userGroups)
        {
           
            if (userGroups != null)
            {
                foreach(string group in userGroups)
                {
                    if (group.Equals("FormAdmins"))
                    {
                        admin = true;
                        break;
                    }
                    else
                    {
                        admin = false;
                    }
                }
             
            }
            else
            {
                admin = false;
            }
        }

    }
}
﻿using Microsoft.Owin.Security.Facebook;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.FacebookAuthProvider
{
    
        public class FacebookAuthProvider : FacebookAuthenticationProvider
        {
            public override Task Authenticated(FacebookAuthenticatedContext context)
            {
                context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
                return Task.FromResult<object>(null);
            }
        
    }
}

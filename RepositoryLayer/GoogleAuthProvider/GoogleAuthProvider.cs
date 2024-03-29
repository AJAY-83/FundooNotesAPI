﻿using Microsoft.Owin.Security.Google;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Result
{
   
        public class GoogleAuthProvider : IGoogleOAuth2AuthenticationProvider
        {
            public void ApplyRedirect(GoogleOAuth2ApplyRedirectContext context)
            {
                context.Response.Redirect(context.RedirectUri);
            }

            public Task Authenticated(GoogleOAuth2AuthenticatedContext context)
            {
                context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
                return Task.FromResult<object>(null);
            }

            public Task ReturnEndpoint(GoogleOAuth2ReturnEndpointContext context)
            {
                return Task.FromResult<object>(null);
            }
        
    }
}

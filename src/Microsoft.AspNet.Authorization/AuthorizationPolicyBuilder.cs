// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Microsoft.AspNet.Authorization
{
    public class AuthorizationPolicyBuilder
    {
        public AuthorizationPolicyBuilder(params string[] activeAuthenticationSchemes)
        {
            AddAuthenticationSchemes(activeAuthenticationSchemes);
        }

        public AuthorizationPolicyBuilder(AuthorizationPolicy policy)
        {
            Combine(policy);
        }

        public IList<IAuthorizationRequirement> Requirements { get; set; } = new List<IAuthorizationRequirement>();
        public IList<string> ActiveAuthenticationSchemes { get; set; } = new List<string>();

        public AuthorizationPolicyBuilder AddAuthenticationSchemes(params string[] activeAuthTypes)
        {
            foreach (var authType in activeAuthTypes)
            {
                ActiveAuthenticationSchemes.Add(authType);
            }
            return this;
        }

        public AuthorizationPolicyBuilder AddRequirements(params IAuthorizationRequirement[] requirements)
        {
            foreach (var req in requirements)
            {
                Requirements.Add(req);
            }
            return this;
        }

        public AuthorizationPolicyBuilder Combine([NotNull] AuthorizationPolicy policy)
        {
            AddAuthenticationSchemes(policy.ActiveAuthenticationSchemes.ToArray());
            AddRequirements(policy.Requirements.ToArray());
            return this;
        }

        public AuthorizationPolicyBuilder RequiresClaim([NotNull] string claimType, params string[] requiredValues)
        {
            return RequiresClaim(claimType, (IEnumerable<string>)requiredValues);
        }

        public AuthorizationPolicyBuilder RequiresClaim([NotNull] string claimType, IEnumerable<string> requiredValues)
        {
            Requirements.Add(new ClaimsAuthorizationRequirement
            {
                ClaimType = claimType,
                AllowedValues = requiredValues
            });
            return this;
        }

        public AuthorizationPolicyBuilder RequiresClaim([NotNull] string claimType)
        {
            Requirements.Add(new ClaimsAuthorizationRequirement
            {
                ClaimType = claimType,
                AllowedValues = null
            });
            return this;
        }

        public AuthorizationPolicyBuilder RequiresRole([NotNull] params string[] roles)
        {
            return RequiresRole((IEnumerable<string>)roles);
        }

        public AuthorizationPolicyBuilder RequiresRole([NotNull] IEnumerable<string> roles)
        {
            RequiresClaim(ClaimTypes.Role, roles);
            return this;
        }

        public AuthorizationPolicyBuilder RequiresUserName([NotNull] string userName)
        {
            RequiresClaim(ClaimTypes.Name, userName);
            return this;
        }

        public AuthorizationPolicyBuilder RequiresAuthenticatedUser()
        {
            Requirements.Add(new DenyAnonymousAuthorizationRequirement());
            return this;
        }

        public AuthorizationPolicy Build()
        {
            return new AuthorizationPolicy(Requirements, ActiveAuthenticationSchemes.Distinct());
        }
    }
}
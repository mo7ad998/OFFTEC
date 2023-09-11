// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                    {
                      Name = "role",
                      UserClaims = new List<string> {"role"}
                    }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api.read"),
                new ApiScope("api.write")
            };

        public static IEnumerable<Client> Clients =>
            new[]
        {
            new Client
            {
                ClientId = "OFFTEC",
                ClientName = "OFFTEC Credentials Client",
                ClientSecrets = { new Secret("OFFTEC".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"api.read", "api.write"}
            }
        };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
          new ApiResource("api")
            {
              Scopes = new List<string> {"api.read", "api.write"},
              ApiSecrets = new List<Secret> {new Secret("OFFTEC".Sha256())},
              UserClaims = new List<string> {"role"}
            }
        };
    }
}
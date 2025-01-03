﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace bizappointment_api.Implementation
{
    public class JWTImplementation
    {
        public string createToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(10);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("test_claim", "STOCKM"),

            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: "http://localhost:57316/", audience: "http://localhost:57316/",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }


        public string GetClaimJson(HttpRequestMessage request, string claimkey)
        {
            var token = "";
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return "";
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));


            SecurityToken securityToken;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidAudience = "http://localhost:57316/",
                ValidIssuer = "http://localhost:57316/",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                LifetimeValidator = this.LifetimeValidator,
                IssuerSigningKey = securityKey
            };
            //extract and assign the user of the jwt
            Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
            HttpContext.Current.User = handler.ValidateToken(token, validationParameters, out securityToken);
            var claims = handler.ValidateToken(token, validationParameters, out securityToken).Claims;
            var targetClaim = claims.FirstOrDefault(c => c.Type == claimkey).Value;

            return targetClaim;
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}
﻿using System.Security.Cryptography.X509Certificates;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;

namespace XeroApi.OAuth
{
    public class XeroApiPartnerSession : OAuthSession
    {
        public XeroApiPartnerSession(string userAgent, string consumerKey, X509Certificate2 signingCertificate, X509Certificate2 sslCertificate)
            : base(CreateConsumerContext(userAgent, consumerKey, signingCertificate))
        {
            ConsumerRequestFactory = new DefaultConsumerRequestFactory(new SimpleCertificateFactory(sslCertificate));
        }

        public XeroApiPartnerSession(string userAgent, string consumerKey, X509Certificate2 signingCertificate, X509Certificate2 sslCertificate,  string accessToken)
            : base(CreateConsumerContext(userAgent, consumerKey, signingCertificate))
        {
            AccessToken = new TokenBase {Token = accessToken};
            ConsumerRequestFactory = new DefaultConsumerRequestFactory(new SimpleCertificateFactory(sslCertificate));
        }

        private static IOAuthConsumerContext CreateConsumerContext(string userAgent, string consumerKey, X509Certificate2 signingCertificate)
        {
            return new OAuthConsumerContext
            {
                // Public apps use HMAC-SHA1
                SignatureMethod = SignatureMethod.RsaSha1,
                UseHeaderForOAuthParameters = true,

                // Urls
                RequestTokenUri = XeroApiEndpoints.PartnerRequestTokenUri,
                UserAuthorizeUri = XeroApiEndpoints.UserAuthorizeUri,
                AccessTokenUri = XeroApiEndpoints.PartnerAccessTokenUri,
                BaseEndpointUri = XeroApiEndpoints.PartnerBaseEndpointUri,
                
                Key = signingCertificate.PrivateKey,
                ConsumerKey = consumerKey,
                ConsumerSecret = string.Empty,
                UserAgent = userAgent,
            };
        }
    }
}
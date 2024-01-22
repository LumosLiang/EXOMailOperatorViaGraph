using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Users.Item.Messages.Item.Copy;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;

namespace EXOMailOperatorViaGraph.Service
{
    public class GraphHelper
    {
        // Settings object
        private static Settings? _settings;
        // App-ony auth token credential
        private static ClientSecretCredential? _clientSecretCredential;
        // Client configured with app-only authentication
        private static GraphServiceClient? _appClient;

        public static void InitializeGraphForAppOnlyAuth(Settings settings)
        {
            _settings = settings;

            // Ensure settings isn't null
            _ = settings ?? throw new System.NullReferenceException("Settings cannot be null");

            //_settings = settings;

            if (_clientSecretCredential == null)
            {
                _clientSecretCredential = new ClientSecretCredential(
                    _settings.TenantId, _settings.ClientId, _settings.ClientSecret);
            }

            if (_appClient == null)
            {
                _appClient = new GraphServiceClient(_clientSecretCredential,
                    // Use the default scope, which will request the scopes
                    // configured on the app registration
                    new[] { "https://graph.microsoft.com/.default" });
            }
        }

        public static async Task<string> GetAppOnlyTokenAsync()
        {
            // Ensure credential isn't null
            _ = _clientSecretCredential ??
                throw new System.NullReferenceException("Graph has not been initialized for app-only auth");

            // Request token with given scopes
            var context = new TokenRequestContext(new[] { "https://graph.microsoft.com/.default" });
            var response = await _clientSecretCredential.GetTokenAsync(context);
            return response.Token;
        }

        public static async Task SendMailAsync(string subject, string body, string recipient)
        {
            // Ensure client isn't null
            _ = _appClient ??
                throw new System.NullReferenceException("Graph has not been initialized for app auth");

            // Create a new message
            var requestBody = new SendMailPostRequestBody
            {
                Message = new Message
                {
                    Subject = subject,
                    Body = new ItemBody
                    {
                        Content = body,
                        ContentType = BodyType.Text
                    },
                    ToRecipients = new List<Recipient>
                    {
                        new Recipient
                        {
                            EmailAddress = new EmailAddress
                            {
                                Address = recipient
                            }
                        }
                    }
                },
            };
        
            // Send the message using specific user
            
            await _appClient.Users[_settings.Mailbox].SendMail.PostAsync(requestBody);
        }

        public static async Task CopyMailAsync(string messageId, string destinationId)
        {
            // Ensure client isn't null
            _ = _appClient ??
                throw new System.NullReferenceException("Graph has not been initialized for app auth");

            // Send the message using specific user

            var requestBody = new CopyPostRequestBody
            {
                DestinationId = destinationId
            };


            var message = await _appClient.Users[_settings.Mailbox].Messages[messageId].GetAsync();

            for(int i = 0; i < 100; i++)
            {
                await _appClient.Users[_settings.Mailbox].Messages[messageId].Copy.PostAsync(requestBody);
            }
        }
    }
}

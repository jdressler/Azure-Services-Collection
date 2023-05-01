using AzureADApi.Models.Models.Configuration;
using AzureADApi.Models.Models.GraphApi;
using AzureADApi.Models.Models.Requests;
using AzureADApi.Services.Interfaces;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Services.Services
{
    public class UserService : IUserService
    {

        private readonly GraphServiceClient _graphServiceClient;
        private readonly AppConfiguration _config;
        public UserService(GraphServiceClient graphServiceClient, AppConfiguration config)
        {
            _graphServiceClient = graphServiceClient;
            _config = config;
        }

        public async Task<List<User>?> GetAdUsers()
        {
            var users = await _graphServiceClient.Users.GetAsync();
            return (users == null || users.Value == null) ? null : users.Value.ToList();
        }

        public async Task CreateAdUser(CreateAdUserRequest request)
        {
            var requestBody = new User  
            {
                AccountEnabled = request.AccountEnabled,
                DisplayName = request.DisplayName,
                MailNickname = request.MailNickname,
                UserPrincipalName = request.Upn + _config.MailDomain,
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true,
                    Password = "kljhh!e5h",
                }
            };

            await _graphServiceClient.Users.PostAsync(requestBody);
        }

        public async Task DisableUser(string upn)
        {
            var request = new User { AccountEnabled = true };
            await _graphServiceClient.Users[upn].PatchAsync(request);
        }

        public async Task<User?> GetAdUserById(string id)
        {
            return await _graphServiceClient.Users[id].GetAsync();
        }
        
        public async Task UpdateAdUserDisplayName(string id, string displayName)
        {
            var request = new User { DisplayName = displayName };
            await _graphServiceClient.Users[id].PatchAsync(request);
        }

    }
}

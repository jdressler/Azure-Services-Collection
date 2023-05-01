using AzureADApi.Models.Models.GraphApi;
using AzureADApi.Models.Models.Requests;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>?> GetAdUsers();
        Task CreateAdUser(CreateAdUserRequest request);
        Task DisableUser(string upn);
        Task<User?> GetAdUserById(string id);
        Task UpdateAdUserDisplayName(string id, string displayName);

    }
}

using AzureADApi.Models.Models.Requests;
using AzureADApi.Models.Models.Responses;
using AzureADApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models.ODataErrors;

namespace AzureADApi.Web.Controllers
{
    [Route("api/adusers")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAdUsers()
        {
            try
            {
                
                var users = await _userService.GetAdUsers();
                if (users != null)
                    return StatusCode(200, new GetAdUsersResponse(false, null, users));
                else
                    return StatusCode(500, new GetAdUsersResponse(true, "Unable to retrieve users", null));
              
            }catch
            {
                return StatusCode(500, new GetAdUsersResponse(true, "Unable to retrieve users", null));
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAdUser(CreateAdUserRequest request)
        {
            try
            {
                await _userService.CreateAdUser(request);
                return StatusCode(200, new CreateAdUserResponse(false, null));
            }
            catch (ODataError ex)
            {
                if(ex.Error != null && ex.Error.Code == "Request_BadRequest")
                {
                    return StatusCode(400, new CreateAdUserResponse(true, ex.Error.Message));
                }
                else
                {
                    return StatusCode(500, new CreateAdUserResponse(true, "Unable to create user"));

                }
            }
            catch
            {
                return StatusCode(500, new CreateAdUserResponse(true, "Unable to create user"));
            }
        }

        [HttpPost]
        [Route("disable")]
        public async Task<IActionResult> DisableUser(DisableAdUserRequest request)
        {
            try
            {
                await _userService.DisableUser(request.Upn);

                return StatusCode(200, new DisableAdUserResponse(false, null));
            }
            catch(ODataError ex)
            {
                if (ex.Error != null && ex.Error.Code == "Request_BadRequest")
                {
                    return StatusCode(400, new DisableAdUserResponse(true, ex.Error.Message));
                }
                else
                {
                    return StatusCode(500, new DisableAdUserResponse(true, "Unable to disable user"));

                }
            }
            catch
            {
                return StatusCode(500, new DisableAdUserResponse(true, "Unable to disable user"));
            }
        
        }

        [HttpGet]
        [Route("get/id")]
        public async Task<IActionResult> GetAdUserByUserId(GetAdUserByIdRequest request)
        {
            try
            {
                var user = await _userService.GetAdUserById(request.Id);

                return StatusCode(200, new GetAdUserByIdResponse(false, null, user));
            }
            catch (ODataError ex)
            {
                if (ex.Error != null && ex.Error.Code == "Request_BadRequest")
                {
                    return StatusCode(400, new GetAdUserByIdResponse(true, ex.Error.Message, null));
                }
                else
                {
                    return StatusCode(500, new GetAdUserByIdResponse(true, "Unable to retrieve user", null));

                }
            }
            catch
            {
                return StatusCode(500, new GetAdUserByIdResponse(true, "Unable to retrieve user", null));
            }
        }

        [HttpPost]
        [Route("update/displayname")]
        public async Task<IActionResult> UpdateAdUserDisplayName(UpdateAdUserDisplayNameRequest request)
        {
            try
            {
                await _userService.UpdateAdUserDisplayName(request.Id, request.DisplayName);
                return StatusCode(200, new UpdateAdUserDisplayNameResponse(false, null));

            }
            catch (ODataError ex)
            {
                if (ex.Error != null && ex.Error.Code == "Request_BadRequest")
                {
                    return StatusCode(400, new UpdateAdUserDisplayNameResponse(true, ex.Error.Message));
                }
                else
                {
                    return StatusCode(500, new UpdateAdUserDisplayNameResponse(true, "Unable to retrieve user"));

                }
            }
            catch
            {
                return StatusCode(500, new UpdateAdUserDisplayNameResponse(true, "Unable to retrieve user"));
            }
        }


    }
}

using DotNetTrainingBatch5.LoginFlowEncrypt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DotNetTrainingBatch5.LoginFlowEncrypt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly EncDecSrvc encDecSrvc;

        public BlogController(EncDecSrvc encDecSrvc)
        {
            this.encDecSrvc = encDecSrvc;
        }

        [HttpPost]
        public IActionResult Login(loginRequest login)
        {
            try
            {
                var result = userStore.users.Where(x => x.username == login.username && x.password == login.password).FirstOrDefault();
                if (result == null)
                {
                    return Unauthorized();
                }
                else
                {
                    var user = new loginModel
                    {
                        sessionExpireTime = DateTime.Now.AddMinutes(1),
                        sessionId = Guid.NewGuid().ToString(),
                        username = result.username
                    };

                    var jsonstring = JsonConvert.SerializeObject(user);
                    var token = encDecSrvc.encryptData(jsonstring);

                    var model = new blogResponseModel
                    {
                        accessToken = token
                    };

                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }

    public static class userStore
    {
        public static List<userData> users = new List<userData>()
        {
            new userData(){username="admin",password="admin"},
            new userData(){username="user1",password="user1"},
            new userData(){username="user2",password="user2"},
        };
    }

    public class  loginRequest
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }

    public class userData
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }

    public class loginModel
    {
        public string? username { get; set; }
        public string? sessionId { get; set; }

        public DateTime? sessionExpireTime { get; set; }
    }

    public class blogResponseModel
    {
        public string? accessToken { get; set; }
    }
}

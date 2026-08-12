using DotNetTrainingBatch5.LoginFlowEncrypt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        [HttpPost("Login")]
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
                        sessionExpireTime = DateTime.Now.AddMinutes(10),
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

        [HttpPost("List")]
        public IActionResult UserList(userListModel model)
        {
            try
            {
                HttpContext.Request.Headers.TryGetValue("token", out var ntoken);

                if (ntoken.Count == 0) return Unauthorized("Access Token required");

                encDecSrvc.decryptData(ntoken.ToString());
                var user = JsonConvert.DeserializeObject<loginModel>(encDecSrvc.decryptData(ntoken.ToString()!));
                if (user.sessionExpireTime < DateTime.Now)
                {
                    return Unauthorized("Session has expired");
                }
                return Ok(userStore.users);
            }
            catch (Exception ex) {
                return StatusCode(500, ex.ToString());
            }          
        }

        [ServiceFilter(typeof(SampleAsyncActionFilter))]
        [HttpPost("List1")]
        public IActionResult UserList1(userListModel model)
        {
            try
            {
                return Ok(userStore.users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }

    public class userListModel
    {
        public string? accessToken { get; set; }
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

    public class SampleAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Do something before the action executes.
            // before
            var result = context.HttpContext.Request.Headers.TryGetValue("token", out var ntoken);

            if (!result)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var encDecSrvc = context.HttpContext.RequestServices.GetService<EncDecSrvc>();
            var jsonString = encDecSrvc?.decryptData(ntoken.ToString());
            var user = JsonConvert.DeserializeObject <loginModel>(jsonString!);

            if(user!.sessionExpireTime < DateTime.Now)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
            // Do something after the action executes.
            // after
        }
    }

    public class ValidationTokenMiddleWare
    {
        private readonly RequestDelegate _next;

        public ValidationTokenMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            // Middleware logic here
            //if(context.Request.Path.StartsWithSegments("/WeatherForecast"))
            //{
            //    goto skip;
            //}
            string requestPath = context.Request.Path.ToString();
            if(!string.IsNullOrEmpty(requestPath) && allowEndPoints.Contains(requestPath))
            {
                goto skip;
            }

            var result = context.Request.Headers.TryGetValue("token", out var ntoken);
            if (!result) 
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var encDecSrvc = context.RequestServices.GetService<EncDecSrvc>();
            var jsonString = encDecSrvc?.decryptData(ntoken.ToString());
            var user = JsonConvert.DeserializeObject<loginModel>(jsonString!);
            if (user!.sessionExpireTime < DateTime.Now)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        skip:
            await _next(context);
        }

        public string[] allowEndPoints = new string[] { "/WeatherForecast", "/api/Blog/Login" };
    }

    public static class ValidationTokenMiddleWareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationTokenMiddleWare>();
        }
    }

}

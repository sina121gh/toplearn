using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using TopLearn.Convertors;
using TopLearn.Core.Services;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
DotNetEnv.Env.Load();

#region Database Context

builder.Services.AddDbContext<TopLearnContext>(options =>
{
    options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
});

#endregion

#region IoC

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IPermisionService, PermisionService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IOrderService, OrderService>();

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

#endregion

//builder.Services.Configure<FormOptions>(options =>
//{
//    options.MultipartBodyLengthLimit = 52428800;
//});

var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.ToString().ToLower().StartsWith("/coursefilesforonlineshow"))
    {
        var callingUrl = context.Request.Headers["Referer"].ToString();
        if (callingUrl != "" && (callingUrl.StartsWith("https://localhost:7168/") || callingUrl.StartsWith("https://localhost:7168/")))
        {
            await next.Invoke();
        }
        else
            context.Response.Redirect("/login");
    }
    else
        await next.Invoke();


});

app.UseStaticFiles();
app.UseRouting();

#region Routes

app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );


app.MapRazorPages();

#endregion

app.UseAuthentication();
app.UseAuthorization();

app.Run();

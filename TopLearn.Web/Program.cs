using Microsoft.EntityFrameworkCore;
using TopLearn.DataLayer.Context;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
DotNetEnv.Env.Load();

#region Database Context
builder.Services.AddDbContext<TopLearnContext>(options =>
{
    options.UseSqlServer();
});
#endregion

var app = builder.Build();


app.UseRouting();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Home}/{Action=Index}/{id?}"
    );

app.Run();

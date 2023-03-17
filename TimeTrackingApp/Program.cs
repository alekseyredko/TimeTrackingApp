using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApp.Infrastructure;
using TimeTrackingApp.Infrastructure.Extensions;
using TimeTrackingApp.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

bool? usePersistentDatabase = builder.Configuration.GetValue<bool?>("usePersistentDatabase");

if (usePersistentDatabase.HasValue && usePersistentDatabase.Value)
{
    builder.Services.AddUnitOfWorkFactory(builder.Configuration);    

    builder.Services.AddDbContext<IdentityContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityContext"));
    });
}
else
{
    builder.Services.AddInMemoryUnitOfWorkFactory(builder.Configuration);

    builder.Services.AddDbContext<IdentityContext>(options =>
    {
        options.UseInMemoryDatabase("IdentityDb");
    });
}

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail= false;
})
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddScoped<AuthenticationStateProvider, IdentityValidationProvider<IdentityUser>>();
builder.Services.AddMediatRDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

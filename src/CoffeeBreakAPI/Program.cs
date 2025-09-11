using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
var jwtSecret = builder.Configuration["JWT__Secret"] ?? "change_this_secret_to_something_long";
var jwtIssuer = builder.Configuration["JWT__Issuer"] ?? "my-app";
var jwtAudience = builder.Configuration["JWT__Audience"] ?? "my-app";
var jwtExpiryMinutes = int.Parse(builder.Configuration["JWT__ExpiryMinutes"] ?? "15");
var refreshTokenExpiryDays = int.Parse(builder.Configuration["REFRESH__ExpiryDays"] ?? "30");

var googleClientId = builder.Configuration["Google__ClientId"];
var googleClientSecret = builder.Configuration["Google__ClientSecret"];

builder.Services.AddControllers();
builder.Services.AddHttpClient();

var key = Encoding.ASCII.GetBytes(jwtSecret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
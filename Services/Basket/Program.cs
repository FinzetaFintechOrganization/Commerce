
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
        opt.Authority = builder.Configuration["IdentityServerUrl"];
        opt.RequireHttpsMetadata = false;
        opt.Audience = "ResourceBasket";
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();



var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapOpenApi();

app.MapScalarApiReference();

app.MapControllers();

app.Run();

using CargasBrasilDB.Extensions;
using CargasBrasilDB.Middlewares;
using CargasBrasilDB.Security;


if (args.Length == 2 && args[0] == "hash-password")
{
    Console.WriteLine(new Pbkdf2PasswordHasher().Hash(args[1]));
    return;
}

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDatabase(builder.Configuration)
    .AddApplicationServices()
    .AddJwtAuthentication(builder.Configuration, builder.Environment)
    .AddAuthRateLimiting()
    .AddApiControllers();

if (builder.Environment.IsDevelopment())
    builder.Services.AddSwaggerWithJwt();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

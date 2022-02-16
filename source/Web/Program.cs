using Architecture.Application;
using Architecture.Database;
using DotNetCore.AspNetCore;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.IoC;
using DotNetCore.Logging;
using DotNetCore.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

builder.Host.UseSerilog();

builder.Services.AddHashService();

builder.Services.AddAuthenticationJwtBearer(new JwtSettings(Guid.NewGuid().ToString(), TimeSpan.FromHours(12)));

builder.Services.AddResponseCompression();

builder.Services.AddControllers().AddJsonOptions().AddAuthorizationPolicy();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSpaStaticFiles("Frontend/dist");

builder.Services.AddContext<Context>(options => options.UseSqlServer("Server=tcp:call-bomb.database.windows.net,1433;Initial Catalog=call-bomb;Persist Security Info=False;User ID=karafra;Password=hywEx3SjKY8Mvx8;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

builder.Services.AddClassesMatchingInterfaces(typeof(IUserService).Assembly, typeof(IUserRepository).Assembly);

var application = builder.Build();

application.UseException();

application.UseHttps();

application.UseRouting();

application.UseResponseCompression();

application.UseAuthentication();

application.UseAuthorization();

application.UseEndpointsMapControllers();

application.UseSwagger();

application.UseSwaggerUI();

application.UseSpaAngular("Frontend", "start", "http://localhost:4200");

application.Run();

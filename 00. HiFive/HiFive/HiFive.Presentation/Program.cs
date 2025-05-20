using HiFive.Infrastructure.Db;
using HiFive.Presentation.Extentions;
using HiFive.Presentation.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer("Data Source=localhost;Initial Catalog=testing_grounds;Integrated Security=True;Encrypt=False",
		b => b.MigrationsAssembly("HiFive.Infrastructure"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<Jwt>(builder.Configuration.GetSection(nameof(Jwt)));
builder.Services.AddOptions<Jwt>()
	.BindConfiguration(nameof(Jwt));

builder.Services.RegisterServices();

builder.Services.AddIdentityAndJwtAuthentication(builder.Configuration);


builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend",
		builder =>
		{
			builder.WithOrigins("http://localhost:5173")
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//using (var scope = app.Services.CreateScope())
//{
//	var unit = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
//	await DbSeeder.Seed(unit);
//}

app.UseMiddleware<BadRequestExceptionHandling>();

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");
app.UseAuthorization();
app.UseAuthorization();


app.MapControllers();

app.Run();



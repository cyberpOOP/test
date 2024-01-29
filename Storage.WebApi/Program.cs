using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Storage.BLL.Mappers;
using Storage.BLL.Services;
using Storage.BLL.Services.Interfaces;
using Storage.DAL;
using Storage.DAL.Repositories;
using Storage.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAnyOrigin",
		builder => builder
			.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod()
	);
});

builder.Services.AddAutoMapper(conf =>
{
	conf.AddProfiles(
		new List<Profile>()
		{
						new OrderItemMappingProfile(),
						new OrderMappingProfile(),
						new ProductMappingProfile(),
						new UserMappingProfile()
		});
});
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowAnyOrigin");

app.MapControllers();
SeedDatabase();
app.Run();

void SeedDatabase()
{
	using (var scope = app.Services.CreateScope())
	{
		var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
		dbInitializer.Seed();
	}
}

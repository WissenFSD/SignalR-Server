
using Microsoft.AspNetCore.SignalR;
using SignalR.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Signal R Kullanacaðýz
builder.Services.AddSignalR();
//builder.Services.AddSingleton<IHubContext<MessageHub>, HubContext<MessageHub>>();

// Cors ekleyelim
builder.Services.AddCors(options =>
{
	options.AddPolicy("Cors", builder =>
	{

		builder.WithOrigins("http://localhost:5063").
		AllowAnyHeader().
		AllowAnyMethod().
		AllowCredentials();

	});
});

var app = builder.Build();

app.Services.GetService(typeof(Microsoft.AspNet.SignalR.IHubContext<MessageHub>));


// Dýþarýdan gelen paketler http//localhost:5050/wissen
// Signal R'in ayný web socketteki gibi hizmet vereceði bir adres olacaktýr. Bu adresi Aspnet Core uygulamasýna Register etmek için bir endpoint ekliyoruz.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

//app.MapControllers();


app.UseCors("Cors");


app.UseRouting();

app.UseEndpoints(endpoints =>
{
	endpoints.MapHub<MessageHub>("/wissen");
	endpoints.MapControllers();
});

app.Run();

using SignalR.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Signal R Kullanaca��z
builder.Services.AddSignalR();

// Cors ekleyelim
builder.Services.AddCors(options =>
{
	options.AddPolicy("Cors", builder =>
	{

		builder.WithOrigins("http://localhost:1010", "http://localhost:2020").
		AllowAnyHeader().
		AllowAnyMethod().
		AllowCredentials();

	});
});

var app = builder.Build();



// D��ar�dan gelen paketler http//localhost:5050/wissen
// Signal R'in ayn� web socketteki gibi hizmet verece�i bir adres olacakt�r. Bu adresi Aspnet Core uygulamas�na Register etmek i�in bir endpoint ekliyoruz.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.UseCors("Cors");


app.UseRouting();

app.UseEndpoints(endpoints =>
{
	endpoints.MapHub<MessageHub>("/wissen");
	//endpoints.MapControllers();
});
app.Run();

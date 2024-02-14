using Microsoft.EntityFrameworkCore;
using MongoDbWithMinimalApi;
using MongoDbWithMinimalApi.Data;
using MongoDbWithMinimalApi.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ProductDbContext>(options => options.UseMongoDB("mongodb://mutas:mutas@localhost:27017",
    "MinimalAPIMongoDb"));
//veritabanı adı


builder.Services.AddScoped<IProductService, ProductService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapProductModelEndPoints();

app.Run();


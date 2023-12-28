using System.Reflection;
using Catalog.API.Data;
using Catalog.API.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
    //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Catalog.API.xml")); // Include XML comments

});



builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json","Catalog.API v1"));
}

app.UseRouting();
app.UseAuthorization();
app.UseRouting().UseEndpoints(endpoint => endpoint.MapControllers());



//app.UseHttpsRedirection();

app.Run();
using Fulfillment.Application.IoC;
using Fulfillment.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddFulfillmentApplication();
builder.Services.AddFulfillmentInfraestructure(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
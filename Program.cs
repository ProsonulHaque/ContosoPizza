var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.Use(async (context, next) =>
{
    //Global exception handler and request logger middleware
    try
    {
        await next();
        Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        Console.WriteLine($"Error occurred for {context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
        Console.WriteLine($"Exception details: {ex}");
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

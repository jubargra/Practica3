var builder = WebApplication.CreateBuilder(args);
string SecretKey = builder.Configuration["Llaves:SecretKey"]!.ToString();


builder.Services.AddControllers().AddJsonOptions(x => { x.JsonSerializerOptions.PropertyNamingPolicy = null; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

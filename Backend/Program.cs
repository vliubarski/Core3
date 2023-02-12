using Backend.DataBase;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOriginPolicy",
        policy =>
        {
            policy.AllowAnyOrigin();
        });
});


builder.Services.Configure<FileStoreDatabaseSettings>(
    builder.Configuration.GetSection("FileStoreDatabase"));
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IMongoDbService, MongoDbService>();
builder.Services.AddTransient<ILogger<MongoDbService>, Logger<MongoDbService>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();

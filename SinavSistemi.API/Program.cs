using Microsoft.EntityFrameworkCore;
using SinavSistemi.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Controller + JSON cycle fix
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ CORS EKLENDİ (EN KRİTİK FIX)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// ✅ CORS PIPELINE
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
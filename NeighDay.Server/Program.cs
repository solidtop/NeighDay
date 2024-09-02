using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeighDay.Server.Common.Exceptions;
using NeighDay.Server.Data;
using NeighDay.Server.Features.Auth;
using NeighDay.Server.Features.Avatars;
using NeighDay.Server.Features.Chats;
using NeighDay.Server.Features.Online;
using NeighDay.Server.Features.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFilter((category, level) =>
            category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)))
        .EnableSensitiveDataLogging();
});

builder.Services.AddSingleton<OnlineStore>();

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOnlineService, OnlineService>();
builder.Services.AddScoped<IAvatarRepository, AvatarRepository>();
builder.Services.AddScoped<IAvatarService, AvatarService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "token";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.WithOrigins("https://127.0.0.1:4200", "https://192.168.1.3:444", "https://78.73.132.91:444")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
}));

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("CorsPolicy");
app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(new ExceptionHandlerOptions()
{
    AllowStatusCode404Response = true,
    ExceptionHandlingPath = "/error"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.MapHub<ChatHub>("/chat");
app.MapHub<OnlineHub>("/online");

app.Run();


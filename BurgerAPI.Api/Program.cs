using BurgerAPI.Api.Data;
using BurgerAPI.Api.Services;
using BurgerAPI.Api.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



string? connectionString = builder.Configuration.GetConnectionString("Burger"); //Nombre conexion en el config del json
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

//Activamos la autentificacion y la autorizacion
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwtOptions =>
        jwtOptions.TokenValidationParameters = TokenService.GetTokenValidationParameters(builder.Configuration));

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddTransient<TokenService>()
                .AddTransient<PasswordService>()
                .AddTransient<AuthService>()
                .AddTransient<BurgerService>()
                .AddTransient<OrderService>();


var app = builder.Build();

#if DEBUG

MigrateDataBase(app.Services);

#endif

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//Activamos el controlador de Usuarios y burgers y pedidos
app.ManageUsers();
app.GetBurgers();
app.saveOrders();

app.Run();


static void MigrateDataBase(IServiceProvider services)
{
    var scope = services.CreateScope();
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    if (dataContext.Database.GetPendingMigrations().Any())
        dataContext.Database.Migrate();
}



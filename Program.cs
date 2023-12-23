using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//se configura el esquema de autenticación por medio de cookies en la aplicación web
builder.Services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication",
    config => {
        config.Cookie.Name = "UserLoginCookie";
        config.LoginPath = "/Usuarios/Login"; // Cambia a la ruta correcta
        config.LogoutPath = "/Home/Index";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(5);//establecemos un cierre de sesion a los 5 minutos

    });
//scrip para interactuar con mi base de datos local
builder.Services.AddDbContext<AppNomina.Models.DbContextAppNomina>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("strConexion")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

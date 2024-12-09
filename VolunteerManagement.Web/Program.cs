using Microsoft.EntityFrameworkCore;
using VolunteerManagement.Infrastructure.Data;
using VolunteerManagement.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar DbContext con SQLite
builder.Services.AddDbContext<VolunteerManagementDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<VolunteerService>();
builder.Services.AddScoped<ActivityService>();
builder.Services.AddScoped<AttendanceReportService>();
builder.Services.AddScoped<OrganizationService>();

// Configurar servicios adicionales
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<VolunteerManagementDbContext>();
    DbInitializer.Initialize(context); // Si tienes un método para inicializar datos
}

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

namespace VolunteerManagement.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(VolunteerManagementDbContext context)
        {
            // Verifica si la base de datos ya tiene tablas
            context.Database.EnsureCreated();  // Asegura que la base de datos esté creada

            // Si no deseas agregar datos predeterminados, simplemente elimina el código que agrega voluntarios.
            // Si ya hay datos, no hace nada.
            if (context.Volunteers.Any()) return;
        }
    }
}

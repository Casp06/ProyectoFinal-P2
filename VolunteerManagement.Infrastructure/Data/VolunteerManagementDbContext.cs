using Microsoft.EntityFrameworkCore;
using VolunteerManagement.Domain.Entities;

namespace VolunteerManagement.Infrastructure.Data
{
    public class VolunteerManagementDbContext : DbContext
    {
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<AttendanceReport> AttendanceReports { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        

        public VolunteerManagementDbContext(DbContextOptions<VolunteerManagementDbContext> options)
            : base(options) { }



    }
}


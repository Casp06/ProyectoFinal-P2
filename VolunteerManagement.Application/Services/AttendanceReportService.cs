using System.Collections.Generic;
using System.Linq;
using VolunteerManagement.Domain.Entities;
using VolunteerManagement.Infrastructure.Data;

namespace VolunteerManagement.Application.Services
{
    public class AttendanceReportService
    {
        private readonly VolunteerManagementDbContext _context;

        public AttendanceReportService(VolunteerManagementDbContext context)
        {
            _context = context;
        }
        public List<Activity> GetActivities()
        {
            return _context.Activities.ToList();  // Asegúrate de que `Activities` sea el nombre de tu DbSet
        }
        public List<Volunteer> GetVolunteers()
        {
            return _context.Volunteers.ToList(); // Asumiendo que tienes una tabla de 'Volunteers'
        }
    public string GetVolunteerName(int volunteerId)
    {
        var volunteer = _context.Volunteers.FirstOrDefault(v => v.Id == volunteerId);
        return volunteer != null ? volunteer.FirstName : "Desconocido";
    }

    public string GetActivityName(int activityId)
    {
        var activity = _context.Activities.FirstOrDefault(a => a.Id == activityId);
        return activity != null ? activity.Name : "Desconocida";
    }
    public AttendanceReport GetById(int id)
    {
        return _context.AttendanceReports
            .FirstOrDefault(r => r.Id == id);
    }
    public IEnumerable<AttendanceReport> GetAll()
    {
        return _context.AttendanceReports.ToList();
    }
        // Obtener todas las organizaciones
        public AttendanceReport GenerateReport(int volunteerId, int activityId)
        {
            var attendance = _context.Attendances
                .Where(a => a.VolunteerId == volunteerId && a.ActivityId == activityId)
                .FirstOrDefault();

            if (attendance == null)
            {
                return new AttendanceReport
                {
                    VolunteerId = volunteerId,
                    ActivityId = activityId,
                    ParticipationDate = DateTime.Now,
                    Notes = "Not Attended" // Notificación si no asistió
                };
            }

            return new AttendanceReport
            {
                VolunteerId = volunteerId,
                ActivityId = activityId,
                ParticipationDate = DateTime.Now,
                Notes = attendance.IsAttended ? "Attended" : "Not Attended" // Asistencia o no asistencia
            };
        }

        // Agregar una nueva organización
        public void AddOrganization(Organization organization)
        {
            _context.Organizations.Add(organization);
            _context.SaveChanges();
        }

        // Actualizar una organización existente
        public void UpdateOrganization(Organization organization)
        {
            var existingOrg = _context.Organizations.Find(organization.Id);
            if (existingOrg != null)
            {
                existingOrg.Name = organization.Name;
                existingOrg.Address = organization.Address;
                existingOrg.PhoneNumber = organization.PhoneNumber;

                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }
        }

        // Generar un reporte de asistencia para todos los voluntarios en una actividad específica
        public List<AttendanceReport> GenerateActivityReport(int activityId)
        {
            var attendances = _context.Attendances
                .Where(a => a.ActivityId == activityId)
                .ToList();

            var reports = attendances.Select(a => new AttendanceReport
            {
                VolunteerId = a.VolunteerId,
                ActivityId = a.ActivityId,
                ParticipationDate = DateTime.Now,
                Notes = a.IsAttended ? "Attended" : "Not Attended" // Asistencia o no asistencia
            }).ToList();

            return reports;
        }

        // Generar un reporte de todas las actividades de un voluntario
        public List<AttendanceReport> GenerateVolunteerReport(int volunteerId)
        {
            var attendances = _context.Attendances
                .Where(a => a.VolunteerId == volunteerId)
                .ToList();

            var reports = attendances.Select(a => new AttendanceReport
            {
                VolunteerId = a.VolunteerId,
                ActivityId = a.ActivityId,
                ParticipationDate = DateTime.Now,
                Notes = a.IsAttended ? "Attended" : "Not Attended" // Asistencia o no asistencia
            }).ToList();

            return reports;
        }
        public void DeleteReport(int id)
        {
            var report = _context.AttendanceReports.Find(id); // Encuentra el reporte por su Id
            if (report != null)
            {
                _context.AttendanceReports.Remove(report); // Elimina el reporte
                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }
        }
        public List<AttendanceReport> GetAllReports()
        {
            return _context.AttendanceReports.ToList();
        }
        public AttendanceReport GetReportById(int id)
        {
            return _context.AttendanceReports.FirstOrDefault(r => r.Id == id);
        }
        public void AddReport(AttendanceReport report)
        {
            _context.AttendanceReports.Add(report);
            _context.SaveChanges();
        }
        public void UpdateReport(AttendanceReport report)
        {
            var existingReport = _context.AttendanceReports.Find(report.Id);
            if (existingReport != null)
            {
                existingReport.VolunteerId = report.VolunteerId;
                existingReport.ActivityId = report.ActivityId;
                existingReport.ParticipationDate = report.ParticipationDate;
                existingReport.Notes = report.Notes;

                _context.SaveChanges();
            }
        }


    }
}

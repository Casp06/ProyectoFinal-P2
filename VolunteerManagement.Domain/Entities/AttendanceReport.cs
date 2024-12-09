namespace VolunteerManagement.Domain.Entities
{
    public class AttendanceReport
    {
        public int Id { get; set; }

        public int ActivityId { get; set; } // Clave foránea para Activ
        public int VolunteerId { get; set; } // Clave foránea para Volunteer

        public DateTime ParticipationDate { get; set; }
        public string Notes { get; set; }
    }

    public class AttendanceReportViewModel
    {
        public IEnumerable<AttendanceReport> Reports { get; set; }
        public List<Volunteer> Volunteers { get; set; }
        public List<Activity> Activities { get; set; }
    }

    public class Attendance
    {
        public int Id { get; set; }
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public bool IsAttended { get; set; } // Propiedad que indica si el voluntario asistió
        public DateTime AttendanceDate { get; set; }
    }

}

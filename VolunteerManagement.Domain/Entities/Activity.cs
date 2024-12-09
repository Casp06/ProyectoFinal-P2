using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VolunteerManagement.Domain.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int OrganizationId { get; set; }
        
 

        // Relación con AttendanceReports y Attendances
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public List<AttendanceReport> AttendanceReports { get; set; } = new List<AttendanceReport>();
    }
}


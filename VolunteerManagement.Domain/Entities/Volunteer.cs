namespace VolunteerManagement.Domain.Entities
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<AttendanceReport> AttendanceReports { get; set; } = new List<AttendanceReport>();
    }
}

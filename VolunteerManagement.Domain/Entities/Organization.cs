namespace VolunteerManagement.Domain.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<Activity> Activities { get; set; } = new List<Activity>();
    }

}
